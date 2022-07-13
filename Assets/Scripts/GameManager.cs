using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Texture2D cursorTexture;
    public PlayerHandler playerHandler;

    public GameObject followCam;
    public GameObject iceDrip;
    public GameObject UICanvas;

    public GameObject healthBar;
    public GameObject tutorialPanel;
    public GameObject endPanel;

    public GameObject pauseCam;
    public GameObject endCam;
    public GameObject optionsPanel;

    public GameObject continueButton;
    public GameObject startButton;

    public GameObject rotationZone;

    public GameObject joystick;
    public GameObject jumpButton;
    public GameObject absorbButton;

    public GameObject stopwatch;

    public event Action OnGameStart;

    private bool hasPaused;

    private Vector2 mousePosition;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        Cursor.visible = false;

        QualitySettings.vSyncCount = 1;
    }

    private void Update()
    {
        mousePosition = InputManager.Instance.PlayerInput.UIMain.MousePosition.ReadValue<Vector2>();
        mousePosition.y = Screen.height - mousePosition.y;

        if (InputManager.Instance.PlayerInput.UIMain.Pause.triggered && InGameUI.Instance)
        {
            if (playerHandler.hasStarted && !hasPaused)
                PauseGame();
            else if (!playerHandler.hasStarted && hasPaused)
                UnpauseGame();
        }
    }

    private IEnumerator WaitForUnpause()
    {
        pauseCam.SetActive(false);
        followCam.SetActive(true);
        for (int i = 0; i < UICanvas.transform.childCount; i++)
        {
            Transform childTransform = UICanvas.transform.GetChild(i);
            
            if (childTransform.gameObject != stopwatch)
                childTransform.gameObject.SetActive(false);
            
            if (childTransform.gameObject == healthBar)
                childTransform.gameObject.SetActive(true);
        }
        if (playerHandler.currentGift != null)
            InGameUI.Instance.GiftPopup();
        yield return new WaitForSeconds(1.5f);
        playerHandler.hasStarted = true;
        iceDrip.SetActive(true);
        hasPaused = false;
        InGameUI.Instance.ToggleStopwatch();
    }

    public void UnpauseGame()
    {
        AudioManager.Instance.PlayUntilFinish("Click");
        Cursor.lockState = CursorLockMode.Locked;
        playerHandler.controller.enabled = true;
        playerHandler.isActive = true;
        StartCoroutine(WaitForUnpause());
    }

    private void PauseGame()
    {
        AudioManager.Instance.PlayUntilFinish("Click");
        playerHandler.hasStarted = false;
        playerHandler.animationManager.ChangeAnimationState(playerHandler.SNOWMAN_RELAXED);
        Cursor.lockState = CursorLockMode.Confined;
        followCam.SetActive(false);
        pauseCam.SetActive(true);
        iceDrip.SetActive(false);
        for (int i = 0; i < UICanvas.transform.childCount; i++)
        {
            Transform childTransform = UICanvas.transform.GetChild(i);
            if (childTransform.gameObject != healthBar && childTransform.gameObject != optionsPanel &&
                childTransform.gameObject != tutorialPanel && childTransform.gameObject != joystick &&
                childTransform.gameObject != rotationZone && childTransform.gameObject != jumpButton &&
                childTransform.gameObject != absorbButton && childTransform.gameObject != endPanel)
                childTransform.gameObject.SetActive(true);
            else
                childTransform.gameObject.SetActive(false);
        }
        InGameUI.Instance.ToggleStopwatch();
        InGameUI.Instance.GiftRemove();
        hasPaused = true;
    }

    private void OnGUI()
    {
        if (Utility.Platform != RuntimePlatform.Android && Utility.Platform != RuntimePlatform.IPhonePlayer && Cursor.lockState != CursorLockMode.Locked)
            GUI.DrawTexture(new Rect(mousePosition.x - (38.4f / 2), mousePosition.y - (44.4f / 2), 38.4f, 44.4f), cursorTexture);
    }

    public void ShowTutorial()
    {
        AudioManager.Instance.PlayUntilFinish("Click");
        AudioManager.Instance.Stop("Main Menu");
        startButton.SetActive(false);
        continueButton.SetActive(true);
        for (int i = 0; i < UICanvas.transform.childCount; i++)
        {
            Transform childTransform = UICanvas.transform.GetChild(i);
            childTransform.gameObject.SetActive(false);
            if (childTransform.name.Equals("Title"))
                Destroy(childTransform.gameObject);
        }
        StartCoroutine(WaitToDisplayTutorial());
    }

    private IEnumerator WaitToDisplayTutorial()
    {
        yield return new WaitForSeconds(0.1f);
        if (Utility.Platform != RuntimePlatform.Android && Utility.Platform != RuntimePlatform.IPhonePlayer)
            tutorialPanel.SetActive(true);
        else
            StartGame();
    }

    private IEnumerator WaitForCamTransition()
    {
        yield return new WaitForSeconds(2f);
        healthBar.SetActive(true);
        iceDrip.SetActive(true);
        rotationZone.SetActive(true);
        if (Utility.Platform != RuntimePlatform.Android && Utility.Platform != RuntimePlatform.IPhonePlayer)
            Cursor.lockState = CursorLockMode.Locked;
        OnGameStart?.Invoke();
    }

    public void StartGame()
    {
        AudioManager.Instance.PlayUntilFinish("Click");
        Destroy(tutorialPanel);
        tutorialPanel = null;
        AudioManager.Instance.Play("Background");

        followCam.SetActive(true);
        StartCoroutine(WaitForCamTransition());
    }

    public void OpenSettings()
    {
        AudioManager.Instance.PlayUntilFinish("Click");
        optionsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        AudioManager.Instance.PlayUntilFinish("Click");
        optionsPanel.SetActive(false);
    }

    public void GameOver()
    {
        InGameUI.Instance.ToggleStopwatch();
        AudioManager.Instance.Stop("Background");
        AudioManager.Instance.Play("Game Over");
        Cursor.lockState = CursorLockMode.None;
        for (int i = 0; i < UICanvas.transform.childCount; i++)
        {
            Transform childTransform = UICanvas.transform.GetChild(i);
            childTransform.gameObject.SetActive(false);
        }
        endPanel.SetActive(true);
        followCam.SetActive(false);
        endCam.SetActive(true);
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        AudioManager.Instance.PlayUntilFinish("Click");
        Application.Quit();
    }
}
