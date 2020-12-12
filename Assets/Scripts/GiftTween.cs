using UnityEngine;

public class GiftTween : MonoBehaviour
{
    public float floatDistance = 1f;
    public float floatDuration = 3f;

    private void Start()
    {
        TweenUp();
    }

    private void TweenUp()
    {
        LeanTween.move(gameObject, gameObject.transform.position + floatDistance * Vector3.up, floatDuration).setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(TweenDown);
    }

    private void TweenDown()
    {
        LeanTween.move(gameObject, gameObject.transform.position - floatDistance * Vector3.up, floatDuration).setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(TweenUp);
    }

    private void OnDisable()
    {
        LeanTween.cancelAll();
    }

    private void OnEnable()
    {
        TweenUp();
    }
}
