using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private IMove movement;
    [SerializeField] private Animator animator;
    [SerializeField] private Avatar idleAvatar;
    [SerializeField] private Avatar slowRunAvatar;
    [SerializeField] private Avatar fastRunAvatar;
    [SerializeField] private Avatar jumpAvatar;
    [SerializeField] private Avatar fallAvatar;
    [SerializeField] private float speedOnFastRun;

    [SerializeField] private Avatar currentAvatar;
    [SerializeField] private bool isFall;
    [SerializeField] private bool isCarried;
    [SerializeField] private bool isCarry;

    public void SetIMove(IMove movement)
    {
        this.movement = movement;
    }

    public void Update()
    {
        if (isCarried) return;

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

        if(isFall)
        {
            ChangeAvatar(fallAvatar);
        }
        else if(jumped)
        {
            ChangeAvatar(jumpAvatar);
        }
        else if (moveSpeed > speedOnFastRun)
        {
            ChangeAvatar(fastRunAvatar);
        }
        else if (moveSpeed < speedOnFastRun && moveSpeed > 0.01)
        {
            ChangeAvatar(slowRunAvatar);
        }
        else
        {
            ChangeAvatar(idleAvatar);
        }
    }

    public void Carry()
    {
        isCarry = true;
        animator.SetBool("Carry", isCarry);
    }

    public void Carried()
    {
        isCarried = true;
        animator.SetBool("Carried", isCarried);
    }

    public void PutPlayer()
    {
        isCarried = false;
        isCarry = false;

        animator.SetBool("Carried", isCarried);
        animator.SetBool("Carried", isCarry);

        animator.SetTrigger("PutPlayer");
    }

    public void Fall()
    {
        isFall = true;
        ChangeAvatar(fallAvatar);
        animator.SetTrigger("Crawl");
    }

    public void Up()
    {
        isFall = false;
        ChangeAvatar(idleAvatar);
        animator.SetTrigger("Up");
    }
    public void Death()
    {
        animator.SetTrigger("Death");
    }

    public void ChangeAvatar(Avatar newAvatar)
    {
        if (currentAvatar == newAvatar) return;

        currentAvatar = newAvatar;

        animator.avatar = newAvatar;
    }
}