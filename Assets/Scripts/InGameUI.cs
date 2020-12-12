using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class InGameUI : MonoBehaviour
{
    public static InGameUI Instance { get; private set; }

    public PlayerHandler playerHandler;
    public Gradient healthGradient;
    public GameObject giftIconPrefab;

    private Slider healthSlider;
    private GameObject giftIcon;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        healthSlider = GetComponent<Slider>();
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
}
