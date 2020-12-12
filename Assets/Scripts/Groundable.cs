using UnityEngine;

public class Groundable : MonoBehaviour
{
    public float gravityPull = 9.81f;
    public float jumpHeight = 2f;
    public Transform groundCheck;
    public LayerMask groundMask;

    protected bool isGrounded;
    public float distanceFromGround = 0.15f;
    protected float ySpeed;

    protected void FreeFall(CharacterController controller)
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, distanceFromGround, groundMask);
        ySpeed -= gravityPull * Time.deltaTime;
        if (isGrounded && ySpeed < 0f)
            ySpeed = -controller.stepOffset / Time.deltaTime;
        controller.Move(new Vector3(0f, 0.5f * ySpeed * Time.deltaTime, 0f));
    }

    protected bool Jump()
    {
        if (isGrounded)
        {
            ySpeed = Mathf.Sqrt(2 * jumpHeight * gravityPull);
            return true;
        }
        return false;
    }
}
