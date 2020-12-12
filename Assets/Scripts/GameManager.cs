using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Texture2D cursorTexture;

    public GameObject followCam;
    public GameObject iceDrip;
    public GameObject UICanvas;

    public GameObject healthBar;
    public GameObject tutorialPanel;
    public GameObject endPanel;

    public GameObject endCam;

    public event Action OnGameStart;

    private Vector2 mousePosition;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        Cursor.visible = false;
    }

    private void Update()
    {
        mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
    }

    private void OnGUI()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            GUI.DrawTexture(new Rect(mousePosition.x - (38.4f / 2), mousePosition.y - (44.4f / 2), 38.4f, 44.4f), cursorTexture);
    }

    public void ShowTutorial()
    {
        AudioManager.Instance.PlayUntilFinish("Click");
        AudioManager.Instance.Stop("Main Menu");
        for (int i = 0; i < UICanvas.transform.childCount; i++)
        {
            Transform childTransform = UICanvas.transform.GetChild(i);
            childTransform.gameObject.SetActive(false);
        }
        StartCoroutine(WaitToDisplayTutorial());
    }

    private IEnumerator WaitToDisplayTutorial()
    {
        yield return new WaitForSeconds(0.1f);
        tutorialPanel.SetActive(true);
    }

    private IEnumerator WaitForCamTransition()
    {
        yield return new WaitForSeconds(2f);
        healthBar.SetActive(true);
        iceDrip.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        OnGameStart?.Invoke();
    }
    public void StartGame()
    {
        AudioManager.Instance.PlayUntilFinish("Click");
        Destroy(tutorialPanel);
        tutorialPanel = null;

        followCam.SetActive(true);
        StartCoroutine(WaitForCamTransition());
    }

    public void GameOver()
    {
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

    public void Quit()
    {
        AudioManager.Instance.PlayUntilFinish("Click");
        Application.Quit();
    }
}
