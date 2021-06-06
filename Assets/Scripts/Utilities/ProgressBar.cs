using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }

    [MenuItem("GameObject/UI/Radial Progress Bar")]
    public static void AddRadialProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Radial Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif

    public bool isLinear;
    public int minimum, maximum;
    public float current;
    public Image mask, fill;
    public Color color;
    public float rotateSpeed;
    private int multiplier = 1;

    private void Start()
    {
        rotateSpeed = 450f;
        GetCurrentFill();
    }

    private void Update()
    {
        if (isLinear)
        {
            GetCurrentFill();
        }
        else
        {
            AnimateRadial();
        }
    }

    private void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
        fill.color = color;
    }

    private void AnimateRadial()
    {
        if (mask.fillMethod == Image.FillMethod.Radial360)
        {
            if (mask.fillAmount >= 0.85f)
            {
                multiplier = -1;
            }
            else if (mask.fillAmount <= 0.075f)
            {
                multiplier = 1;
            }
            mask.fillAmount += Time.deltaTime * multiplier;
            mask.transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.forward);
        }
    }
}
