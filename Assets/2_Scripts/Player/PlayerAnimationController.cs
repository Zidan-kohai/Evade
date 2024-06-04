using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private const string RunSpeedKey = "RunSpeed";
    private const string JumpingKey = "Jumping";
    private const string DeathKey = "Death";
    private const string CarryKey = "Carry";
    private const string CarriedKey = "Carried";
    private const string PutPlayerKey = "PutPlayer";
    private const string CrawlKey = "Crawl";
    private const string UpKey = "Up";
    private const string FallKey = "Fall";

    [SerializeField] private IMove movement;
    [SerializeField] private Animator animator;
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

        animator.SetFloat(RunSpeedKey, moveSpeed);

        
        bool jumped = movement.IsJump();

        if (jumped)
        {
            animator.SetBool(JumpingKey, true);
        }
        else
        {
            animator.SetBool(JumpingKey, false);
        }
    }

    public void Carry()
    {
        isCarry = true;
        animator.SetBool(CarryKey, isCarry);
    }

    public void Carried()
    {
        isCarried = true;
        animator.SetBool(CarriedKey, isCarried);
    }

    public void PutPlayer()
    {
        isCarried = false;
        isCarry = false;

        animator.SetBool(CarriedKey, isCarried);
        animator.SetBool(CarryKey, isCarry);

        animator.SetTrigger(PutPlayerKey);
    }

    public void Fall()
    {
        isFall = true;
        animator.SetTrigger(CrawlKey);
        animator.SetBool(FallKey, isFall);
    }

    public void Up()
    {
        isFall = false;
        animator.SetTrigger(UpKey);
        animator.SetBool(FallKey, isFall);
    }
    public void Death()
    {
        animator.SetTrigger(DeathKey);
    }
}