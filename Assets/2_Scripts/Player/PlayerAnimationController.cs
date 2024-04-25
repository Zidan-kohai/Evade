using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private IMove movement;
    [SerializeField] private Animator animator;
    [SerializeField] private Avatar idleAvatar;
    [SerializeField] private Avatar slowRunAvatar;
    [SerializeField] private Avatar fastRunAvatar;
    [SerializeField] private Avatar jumpAvatar;

    private Avatar currentAvatar;

    public void SetIMove(IMove movement)
    {
        this.movement = movement;
    }

    public void Update()
    {
        float moveSpeed = movement.MoveSpeed();

        animator.SetFloat("RunSpeed", moveSpeed);

        if(moveSpeed > 5)
        {
            ChangeAvatar(fastRunAvatar);
        }
        else if(moveSpeed < 5 && moveSpeed > 0.01)
        {
            ChangeAvatar(slowRunAvatar);
        }
        else
        {
            ChangeAvatar(idleAvatar);
        }
        bool jumped = movement.IsJump();

        if (jumped)
        {
            Debug.Log("Jumped");
            animator.SetBool("Jumping", true);
            ChangeAvatar(jumpAvatar);
        }
        else
        {
            animator.SetBool("Jumping", false);
        }
    }


    public void ChangeAvatar(Avatar newAvatar)
    {
        if (currentAvatar == newAvatar) return;

        currentAvatar = newAvatar;

        animator.avatar = newAvatar;
    }
}