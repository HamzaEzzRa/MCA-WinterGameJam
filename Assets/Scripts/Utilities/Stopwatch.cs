using UnityEngine;
using System;

public class Stopwatch : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI targetText = default;

    private bool active = false;
    private float currentTime;

    private void Start()
    {
        currentTime = 0f;
    }

    private void Update()
    {
        if (active) {
            currentTime += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            targetText.SetText(timeSpan.ToString(@"mm\:ss\:fff"));
        }
    }

    public void ToggleStopwatch()
    {
        active = !active;
    }

    public long GetScore()
    {
        return (long) (currentTime * 1000);
    }
}
