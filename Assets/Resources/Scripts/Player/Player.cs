using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    bool isIdle;

    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public bool IsMoving { get; private set; } = true;
    float horizontalInput;

    bool isFacingRight = true;

    [Header("Jump Settings")]
    [SerializeField] float jumpForce;
    [SerializeField] private float gravityForce = 0.5f;
    [SerializeField] private float ascentGravityScale = 0.8f;
    [SerializeField] private float descentGravityScale = 2.0f;
    [SerializeField] private float peakGravityScale = 0.2f;
    [SerializeField] private float peakThreshold = 0.1f;
    [SerializeField] private float coyoteTimeDuration = 0.2f;
    public float CoyoteTimeTimer { get; set; }
    private bool isJumping;
    private bool isFalling;

    [Header("Wall Mechanics")]
    public float wallSlidingSpeed = 2f;
    [SerializeField] private float wallJumpingTime = 0.4f;
    public Vector2 wallJumpingPower = new(8, 16);
    bool isWallSliding;
    private bool isWallJumping;
    public bool WallJumpRequested { get; set; }
    public float wallJumpingDirection;
    public float wallJumpingTimer;
    [Header("Checks")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    [Header("Combat")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayers;

    [Header("Animation")]
    public Animator animator;
    private string currentAnimationState;

    public const string Player_Idle = "Player_Idle";
    public const string Player_Run = "Player_Run";
    public const string Player_Attack = "Player_Attack";
    public const string Player_Jump = "Player_Jump";
    public const string Player_Fall = "Player_Fall";
    public const string Player_WallSlide = "Player_WallSlide";
    public const string Player_Hurt = "Player_Hurt";
    public const string Player_Death = "Player_Death";

    [Header("State Machine")]
    public PlayerStateMachine stateMachine;
    [SerializeField] PlayerStateID initialState;
    private bool isRunning;
    [SerializeField] float wallJumpingDuration;
    [SerializeField] LayerMask attackLayer;
    private bool startWallJump;


    [Header("Health System")]
    [SerializeField] GameObject healthPoint;
    List<GameObject> healthPoints;
    [SerializeField] Transform healthUICanvas;
    bool isHurt;
    bool isDead;

    #region Start, Update, FixedUpdate
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        RegisterAllStates();
        healthPoints = new List<GameObject>();
        InitializeHP(3);
    }

    private void Update()
    {

        //Debug.Log("is grounded = " + IsGrounded());
        if (!isWallJumping && !isWallSliding)
        {
            FlipCharacter();
        }
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
        AdjustGravity();
        //HandleCoyoteTime();
        HandleWallSlide();
        //Debug.Log("Wall Jumping Direction: " + wallJumpingDirection);
        //Debug.Log("Velocity Y: " + rb.velocity.y);
    }

    void RegisterAllStates()
    {
        stateMachine = new PlayerStateMachine(this);
        stateMachine.RegisterState(new PlayerIdleState());
        stateMachine.RegisterState(new PlayerRunState());
        stateMachine.RegisterState(new PlayerFallState());
        stateMachine.RegisterState(new PlayerJumpState());
        stateMachine.RegisterState(new PlayerWallJumpState());
        stateMachine.RegisterState(new PlayerWallSlideState());
        stateMachine.RegisterState(new PlayerAttackState());
        stateMachine.RegisterState(new PlayerHurtState());
        stateMachine.RegisterState(new PlayerDeathState());
        stateMachine.ChangeState(initialState);
    }
    #endregion

    #region Movement
    public void Move(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>().x;
    }

    public float HorizontalInput
    {
        get { return horizontalInput; }
        set { horizontalInput = value; }
    }

    public bool IsRunning
    {
        get { return isRunning; }
        set { isRunning = value; }
    }

    public bool IsIdle
    {
        get { return isIdle; }
        set { isIdle = value; }
    }

    public bool IsFacingRight
    {
        get { return isFacingRight; }
        set { isFacingRight = value; }
    }
    public void MovePlayer()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        //Debug.Log($"Moving player: Horizontal = {Horizontal}, Velocity = {rb.velocity}");
    }
    #endregion

    #region Jump
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && !isDead)
        {
            stateMachine.ChangeState(PlayerStateID.Jump);
        }
    }

    void PlayerJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    public bool IsJumping
    {
        get { return isJumping; }
        set { isJumping = value; }
    }

    public float JumpForce
    {
        get { return jumpForce; }
        set { jumpForce = value; }
    }

    public bool IsFalling
    {
        get { return isFalling; }
        set { isFalling = value; }
    }
    #endregion

    #region Wall Jump

    public void WallJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsWallSliding)
        {
            stateMachine.ChangeState(PlayerStateID.WallJump);
            startWallJump = true;
        }
    }

    public float WallJumpTimer
    {
        get { return wallJumpingTimer; }
        set { wallJumpingTimer = value; }
    }
    public float WallJumpDuration
    {
        get { return wallJumpingTime; }
        set { wallJumpingTime = value; }
    }

    public bool IsWallJumping
    {
        get { return isWallJumping; }
        set { isWallJumping = value; }
    }

    public bool StartWallJump
    {
        get { return startWallJump; }
        set { startWallJump = value; }
    }
    #endregion

    #region Attack
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && !isDead)
        {
            stateMachine.ChangeState(PlayerStateID.Attack);
        }
    }

    public void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, attackLayer);

        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            enemy.stateMachine.ChangeState(EnemyStateID.Hurt);
            Debug.Log("Enemy has been hit: " + hit.gameObject.name); // Optional debug
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
    #endregion

    private void InitializeHP(float totalHP)
    {
        float spacing = 75;
        Vector3 startPos = new Vector3 (50, -50, 0);
        

        for (int i = 0; i < totalHP; i++)
        {
            GameObject hp = Instantiate(healthPoint, healthUICanvas);
            hp.GetComponent<RectTransform>().anchoredPosition = startPos + new Vector3(spacing * i, 0, 0);
            healthPoints.Add(hp);
        }
    }

    public List<GameObject> HealthPoints
    {
        get { return healthPoints; }
        set { healthPoints = value; }
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }
    public IEnumerator StopMoving()
    {
        IsMoving = false;
        yield return new WaitForSeconds(wallJumpingTime);
        IsMoving = true;
    }
    private void AdjustGravity()
    {
        if (Mathf.Abs(rb.velocity.y) < peakThreshold && rb.velocity.y > 0)
        {
            rb.gravityScale = peakGravityScale;
        }
        else if (rb.velocity.y > 0)
        {
            rb.gravityScale = ascentGravityScale;
        }
        else
        {
            rb.gravityScale = descentGravityScale;
        }
    }

    private void HandleCoyoteTime()
    {
        if (IsGrounded())
        {
            ResetCoyoteTime();
        }
        else
        {
            CoyoteTimeTimer -= Time.fixedDeltaTime;
        }
    }

    public void ResetCoyoteTime()
    {
        CoyoteTimeTimer = coyoteTimeDuration;
    }

    public void HandleWallSlide()
    {
        if (IsWalled() && !IsGrounded() && !isWallSliding)
        {
            stateMachine.ChangeState(PlayerStateID.WallSlide);
        }
    }

    public bool IsWallSliding
    {
        get { return isWallSliding; }
        set { isWallSliding = value; }
    }

    public void ChangeAnimationState(string newAnimationState)
    {
        if (currentAnimationState == newAnimationState) return;
        animator.Play(newAnimationState);
        currentAnimationState = newAnimationState;
        //Debug.Log("Current Animation: " +  newAnimationState);
    }

    public void FlipCharacter()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void FlipCharacter_Alt()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    
    

    public bool IsHurt
    {
        get { return isHurt; }
        set { isHurt = value; }
    }
}
