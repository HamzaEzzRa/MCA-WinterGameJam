using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Stopwatch stopwatch = default;

    public static InGameUI Instance { get; private set; }

    public PlayerHandler playerHandler;
    public Gradient healthGradient;
    public GameObject giftIconPrefab;
    public Slider healthSlider;

    private GameObject giftIcon;

    private void Start()
    {
        if (Instance == null)
            Instance = this;

        GameManager.Instance.OnGameStart += InitializeStopwatch;

        SetMaxHealth();
    }

    public void SetMaxHealth()
    {
        healthSlider.maxValue = playerHandler.startingHealth;
        SetHealth(playerHandler.startingHealth);

        healthSlider.fillRect.gameObject.GetComponent<Image>().color = healthGradient.Evaluate(1f);
    }

    public void SetHealth(float remainingHealth)
    {
        healthSlider.value = remainingHealth;
        healthSlider.fillRect.gameObject.GetComponent<Image>().color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }

    public void GiftPopup()
    {
        giftIcon = Instantiate(giftIconPrefab, GetComponentInParent<Canvas>().gameObject.transform);
    }

    public void GiftRemove()
    {
        if (giftIcon != null)
            Destroy(giftIcon);
    }

    public void ToggleStopwatch()
    {
        stopwatch.ToggleStopwatch();
    }

    public long GetStopwatchScore()
    {
        return stopwatch.GetScore();
    }

    private void InitializeStopwatch()
    {
        GameManager.Instance.OnGameStart -= InitializeStopwatch;
        stopwatch.gameObject.SetActive(true);
        stopwatch.ToggleStopwatch();
    }
}
