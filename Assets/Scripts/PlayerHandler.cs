using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
public class PlayerHandler : LivingEntity
{
    //private Camera mainCamera;

    public float moveSpeed = 2.5f;
    public float turnSmoothTime = 0.1f;
    public float timeBetweenIdles = 0.5f;

    public float continuousDamage = 10f;
    public float timeUntilSnowSpawn = 1f;
    public float timeBetweenWalkSounds = 0.5f;

    public bool hasStarted;
    public bool isActive;

    public GameObject snowPatchPrefab;
    public GameObject corpsePrefab;
    public ParticleSystem dripSystem;

    public List<Transform> checkpointTransformList = new List<Transform>();
    public List<Vector3> checkpointRotationList = new List<Vector3>();
    public List<Transform> checkpointFields = new List<Transform>();

    public Transform knobTransform;
    public Transform platformTransform;

    public int CorpseCount { get => corpseStack.Count; }

    [HideInInspector] public CharacterController controller;
    [HideInInspector] public GameObject currentGift;

    private float moveTurnSmoothVelocity;
    //private float cameraTurnSmoothVelocity;
    private float lastIdleTime;
    
    private float lastSnowSpawnTime;
    private float lastWalkSoundTime;

    private Vector3 currentCheckpointPosition;
    private Vector3 currentCheckpointRotation;
    private Vector3 giftOriginalPosition;

    private Vector3 originalKnobPosition;
    private Vector3 originalPlatformPosition;

    private float damageMultiplier;
    private int currentCheckpointIndex = 0;

    private bool cheatActivated;
    private bool isPlatformActive;

    private Stack<GameObject> corpseStack = new Stack<GameObject>();

    [HideInInspector] public AnimationManager animationManager;

    public readonly int SNOWMAN_RELAXED = Animator.StringToHash("Snowman Relaxed");
    private readonly int SNOWMAN_IDLE_1 = Animator.StringToHash("Snowman Idle 1");
    private readonly int SNOWMAN_IDLE_2 = Animator.StringToHash("Snowman Idle 2");
    private readonly int SNOWMAN_IDLE_2_INV = Animator.StringToHash("Snowman Idle 2 Inv");
    private readonly int SNOWMAN_IDLE_3 = Animator.StringToHash("Snowman Idle 3");
    private readonly int SNOWMAN_JUMP = Animator.StringToHash("Snowman Jump");
    private readonly int SNOWMAN_WALK = Animator.StringToHash("Snowman Walk");
    private readonly int SNOWMAN_RETRACT = Animator.StringToHash("Snowman Retract");
    private readonly int SNOWMAN_EXTEND = Animator.StringToHash("Snowman Extend");

    public void StartGameLogic()
    {
        GameManager.Instance.OnGameStart -= StartGameLogic;
        hasStarted = true;
    }

    protected override void Start()
    {
        base.Start();
        hasStarted = false;
        isActive = true;

        //mainCamera = Camera.main;

        controller = GetComponent<CharacterController>();
        animationManager = GetComponent<AnimationManager>();

        GameManager.Instance.OnGameStart += StartGameLogic;
        OnDeath += Respawn;

        lastIdleTime = 0f;
        damageMultiplier = 1f;

        currentCheckpointPosition = transform.position;
        currentCheckpointRotation = -20f * Vector3.up;
        currentCheckpointIndex = 0;
        currentGift = null;

        lastSnowSpawnTime = 0f;
        lastWalkSoundTime = 0f;

        originalKnobPosition = knobTransform.position;
        originalPlatformPosition = platformTransform.position;
    }

    private void Update()
    {
        if (!hasStarted)
        {
            IdleControl(Vector3.zero);
            if (InputManager.Instance.PlayerInput.UIMain.CheatCode.triggered)
            {
                cheatActivated = !cheatActivated;
                if (!cheatActivated)
                    health = 100f;
            }
        }

        else
        {
            if (cheatActivated)
            {
                health = 99999f;
                moveSpeed = 5f;
            }

            if (isActive)
            {
                if (InputManager.Instance.PlayerInput.PlayerMain.Absorb.triggered && corpseStack.Count > 0)
                {
                    health += startingHealth * 0.25f;
                    health = Mathf.Clamp(health, 0f, startingHealth * 1.25f);

                    GameObject corpse = corpseStack.Pop();
                    Destroy(corpse);
                }


                if (Time.time >= lastSnowSpawnTime + timeUntilSnowSpawn && isGrounded)
                {
                    lastSnowSpawnTime = Time.time;
                    SpawnIcePatch();
                }

                MouseStateControl();
                FreeFall(controller);
                /*PlayerMouseTurn(mainCamera.transform.eulerAngles.y);*/

                TakeDamage(continuousDamage * damageMultiplier * Time.deltaTime);
            }
        }
    }

    public new bool Jump()
    {
        bool value = base.Jump();
        if (value)
        {
            lastIdleTime = Time.time;
            animationManager.OverrideAnimationState(SNOWMAN_JUMP);
        }
        return value;
    }

    public void PlayerMove(Vector3 inputDirection)
    {
        inputDirection = inputDirection.normalized;

        if (inputDirection.sqrMagnitude >= 0.01f)
        {
            float turnAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, turnAngle, ref moveTurnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, turnAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
            if (isGrounded)
            {
                animationManager.ChangeAnimationState(SNOWMAN_WALK);
                if (Time.time >= lastWalkSoundTime + timeBetweenWalkSounds)
                {
                    AudioManager.Instance.PlayUntilFinish("Walk");
                    lastWalkSoundTime = Time.time;
                }
            }
            else
            {
                animationManager.ChangeAnimationState(SNOWMAN_JUMP);
                AudioManager.Instance.Stop("Walk");
                lastWalkSoundTime = Time.time - timeBetweenWalkSounds;
            }
            lastIdleTime = Time.time;
            lastSnowSpawnTime = Time.time;
        }
        else
        {
            animationManager.ChangeAnimationState(SNOWMAN_RELAXED);
            AudioManager.Instance.Stop("Walk");
            lastWalkSoundTime = Time.time - timeBetweenWalkSounds;
        }
    }

    /*private void PlayerMouseTurn(float point)
    {
        if (Cursor.lockState == CursorLockMode.Locked && controller.velocity.x == 0 && controller.velocity.z == 0)
        {
            Vector2 lookInput = InputManager.Instance.PlayerInput.PlayerMain.Look.ReadValue<Vector2>();
            if (lookInput.x != 0f || lookInput.y != 0f)
            {
                float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, point, ref cameraTurnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
                if (controller.velocity.y != 0)
                    lastIdleTime = Time.time;
            }
        }
    }*/

    public void IdleControl(Vector3 movement)
    {
        if (Time.time >= lastIdleTime + timeBetweenIdles && movement == Vector3.zero)
        {
            int idleIndex = Random.Range(1, 5);
            int idleHash = SNOWMAN_IDLE_1;
            switch (idleIndex)
            {
                case 2:
                    idleHash = SNOWMAN_IDLE_2;
                    break;
                case 3:
                    idleHash = SNOWMAN_IDLE_2_INV;
                    break;
                case 4:
                    idleHash = SNOWMAN_IDLE_3;
                    break;
                default:
                    break;
            }
            lastIdleTime = Time.time;
            animationManager.ChangeAnimationState(idleHash);
        }
    }

    private void MouseStateControl()
    {
        if (InputManager.Instance.PlayerInput.UIMain.MouseFocus.triggered)
        {
            if (Cursor.lockState != CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void StashGift(GameObject gift)
    {
        AudioManager.Instance.PlayUntilFinish("Gift Action");
        checkpointFields[currentCheckpointIndex].gameObject.SetActive(true);
        giftOriginalPosition = gift.transform.position;
        currentGift = gift;
        gift.SetActive(false);
        InGameUI.Instance.GiftPopup();
    }

    private void PopbackGift()
    {
        if (currentGift != null)
        {
            checkpointFields[currentCheckpointIndex].gameObject.SetActive(false);
            InGameUI.Instance.GiftRemove();
            currentGift.transform.position = giftOriginalPosition;
            currentGift.SetActive(true);
            currentGift = null;
        }
    }

    private void DeliverGift()
    {
        if (currentGift != null)
        {
            AudioManager.Instance.PlayUntilFinish("Gift Action");
            checkpointFields[currentCheckpointIndex].gameObject.SetActive(false);
            InGameUI.Instance.GiftRemove();
            Destroy(currentGift);
            currentCheckpointPosition = checkpointTransformList[currentCheckpointIndex].position;
            currentCheckpointRotation = checkpointRotationList[currentCheckpointIndex];
            currentCheckpointIndex++;
            if (currentCheckpointIndex == checkpointTransformList.Count)
            {
                hasStarted = false;
                isActive = false;
                transform.rotation = Quaternion.Euler(currentCheckpointRotation);
                animationManager.ChangeAnimationState(SNOWMAN_RELAXED);
                GameManager.Instance.GameOver();
            }
        }
    }

    private void SpawnIcePatch()
    {
        AudioManager.Instance.PlayUntilFinish("Ice Forming");
        GameObject newSnowPatch = Instantiate(snowPatchPrefab);
        newSnowPatch.transform.position = transform.position;
    }

    private IEnumerator UntilGroundedRespawn()
    {
        float waitTime = 0f;

        while (isGrounded == false)
        {
            yield return null;
            waitTime += Time.deltaTime;
            if (waitTime >= 0.75f)
                break;
        }

        isActive = false;
        controller.enabled = false;
        animationManager.isActive = false;

        SpawnIcePatch();

        GameObject newCorpse = Instantiate(corpsePrefab);
        newCorpse.transform.position = transform.position;
        newCorpse.transform.rotation = transform.rotation;

        corpseStack.Push(newCorpse);

        AnimationManager corpseAnimationManager = newCorpse.GetComponent<AnimationManager>();
        corpseAnimationManager.isActive = true;

        DeathAnimationFinished();

        corpseAnimationManager.ChangeAnimationState(SNOWMAN_RETRACT);
    }

    private void Respawn()
    {
        StartCoroutine(UntilGroundedRespawn());
    }

    public void DeathAnimationFinished()
    {
        transform.position = currentCheckpointPosition;
        transform.rotation = Quaternion.Euler(currentCheckpointRotation);

        PopbackGift();

        animationManager.isActive = true;
        animationManager.ChangeAnimationState(SNOWMAN_EXTEND);

        dead = false;
        health = startingHealth;
        InGameUI.Instance.SetMaxHealth();
    }

    public void RespawnAnimationFinished()
    {
        animationManager.ChangeAnimationState(SNOWMAN_RELAXED);

        isActive = true;
        controller.enabled = true;

        lastIdleTime = Time.time;
        lastSnowSpawnTime = Time.time;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var main = dripSystem.main;
        if (hit.collider.CompareTag("Ice") || hit.collider.CompareTag("Corpse"))
        {
            damageMultiplier = 0.25f;
            main.gravityModifier = 0.25f;
            moveSpeed = 3.25f;
        }
        else if (hit.collider.CompareTag("Water"))
        {
            damageMultiplier = 2f;
            main.gravityModifier = 0.75f;
            moveSpeed = 2f;
        }
        else
        {
            damageMultiplier = 1f;
            main.gravityModifier = 0.5f;
            moveSpeed = 2.5f;
        }
        if (hit.collider.CompareTag("Gift") && currentGift == null)
            StashGift(hit.collider.gameObject);
        if (hit.collider.CompareTag("Checkpoint") && currentGift != null && hit.collider.gameObject.transform == checkpointTransformList[currentCheckpointIndex])
            DeliverGift();
        if (hit.collider.CompareTag("Button") && !isPlatformActive)
        {
            LeanTween.move(knobTransform.gameObject, originalKnobPosition - 0.125f * Vector3.up, 0.5f);
            AudioManager.Instance.Play("Moving Platform");
            LeanTween.move(platformTransform.gameObject, originalPlatformPosition + 5f * Vector3.up, 12.5f)
                .setOnComplete(() => AudioManager.Instance.Stop("Moving Platform"));
            isPlatformActive = true;
            AudioManager.Instance.PlayUntilFinish("Button");
        }
    }

    private void OnDestroy()
    {
        OnDeath -= Respawn;
    }
}
