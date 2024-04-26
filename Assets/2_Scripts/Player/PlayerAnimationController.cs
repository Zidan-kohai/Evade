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

        
        bool jumped = movement.IsJump();

        if (jumped)
        {
            animator.SetBool("Jumping", true);
        }
        else
        {
            animator.SetBool("Jumping", false);
        }


        if(jumped)
        {
            ChangeAvatar(jumpAvatar);
        }
        else if (moveSpeed > 10)
        {
            ChangeAvatar(fastRunAvatar);
        }
        else if (moveSpeed < 10 && moveSpeed > 0.01)
        {
            ChangeAvatar(slowRunAvatar);
        }
        else
        {
            ChangeAvatar(idleAvatar);
        }
    }


    public void ChangeAvatar(Avatar newAvatar)
    {
        if (currentAvatar == newAvatar) return;

        currentAvatar = newAvatar;

        animator.avatar = newAvatar;
    }
}