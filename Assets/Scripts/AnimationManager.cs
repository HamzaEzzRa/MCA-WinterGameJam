using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [HideInInspector] public int currentState;
    [HideInInspector] public bool isActive;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isActive = true;
    }

    public void ChangeAnimationState(int newState)
    {
        if (isActive)
        {
            if (animator == null)
                animator = GetComponent<Animator>();
            if (newState == currentState)
                return;
            animator.Play(newState);
            currentState = newState;
        }
    }

    public void OverrideAnimationState(int newState)
    {
        if (isActive)
        {
            animator.Play(newState);
            currentState = newState;
        }
    }
}
