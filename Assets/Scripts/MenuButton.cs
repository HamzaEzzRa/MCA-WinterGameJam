using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Color idleColor;
    public Color hoverColor;
    public Color pressColor;

    private TMP_Text targetText;

    private void Start()
    {
        targetText = GetComponentInChildren<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetText.color = idleColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        targetText.color = pressColor;
    }
}
