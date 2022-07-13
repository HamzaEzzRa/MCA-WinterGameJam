using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public PlayerInput PlayerInput { get; private set; }

    [SerializeField] private PlayerHandler playerHandler = default;

    private Vector3 movement;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        PlayerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        PlayerInput.Enable();
    }

    private void OnDisable()
    {
        PlayerInput.Disable();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStart += OSInput;
    }

    private void Update()
    {
        if (playerHandler.hasStarted && playerHandler.isActive)
        {
            Vector2 input = PlayerInput.PlayerMain.Move.ReadValue<Vector2>();
            movement = new Vector3(input.x, 0f, input.y);

            if (PlayerInput.PlayerMain.Jump.triggered)
                playerHandler.Jump();

            playerHandler.IdleControl(movement);
            playerHandler.PlayerMove(movement);

            if (GameManager.Instance.absorbButton.activeInHierarchy)
                GameManager.Instance.absorbButton.GetComponent<Button>().interactable = playerHandler.CorpseCount > 0;
        }
    }

    private void OSInput()
    {
        GameManager.Instance.OnGameStart -= OSInput;

        if (Utility.Platform == RuntimePlatform.Android || Utility.Platform == RuntimePlatform.IPhonePlayer)
        {
            GameManager.Instance.joystick.SetActive(true);
            GameManager.Instance.jumpButton.SetActive(true);
            GameManager.Instance.absorbButton.SetActive(true);
        }
    }
}
