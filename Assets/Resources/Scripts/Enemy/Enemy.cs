using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool isFacingRight = true;
    public Animator animator;
    private string currentAnimationState;

    public const string Enemy_Idle = "Enemy_Idle";
    public const string Enemy_Run = "Enemy_Run";
    public const string Enemy_Attack = "Enemy_Attack";
    public const string Enemy_Hurt = "Enemy_Hurt";
    public const string Enemy_Fall = "Enemy_Fall";
    public const string Enemy_Death = "Enemy_Death";

    [Header("Movement Settings")]
    bool isPatrolling;
    [SerializeField] bool startPatrol;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform pointA;
    [SerializeField] float pointA_Radius;
    [SerializeField] Transform pointB;
    [SerializeField] float pointB_Radius;
    Transform currentPoint;
    float currentPointDirection;

    public EnemyStateMachine stateMachine;
    [SerializeField] EnemyStateID initialState;

    [Header("Chase Player Settings")]
    [SerializeField] Transform pointOfVision;
    bool playerInSight = false;
    float directionToPlayer;
    bool isChasing;
    [SerializeField] float visionDistance;
    [SerializeField] LayerMask playerLayer;
    Player player;

    [Header("Checks")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] float knockBackForce;
    bool isHurt;


    [Header("Attack Settings")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask attackLayer;
    private bool isAttacking;


    [Header("Health System")]
    HealthSystem_Enemy healthSystem;
    bool isDead;
    #region Start, Update, FixedUpdate
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new EnemyStateMachine(this);
        stateMachine.RegisterState(new EnemyIdleState());
        stateMachine.RegisterState(new EnemyRunState());
        stateMachine.RegisterState(new EnemyHurtState());
        stateMachine.RegisterState(new EnemyAttackState());
        stateMachine.RegisterState(new EnemyPatrolState());
        stateMachine.RegisterState(new EnemyChaseState());
        stateMachine.RegisterState(new EnemyDeathState());
        stateMachine.ChangeState(initialState);

        currentPoint = pointA;

        healthSystem = new HealthSystem_Enemy(100);
    }
    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
        if (!isHurt && !isDead)
        {
            DetectPlayer();

            if (!IsAttacking)
            {
                CheckRightOrLeft();
            }
        }
    }

    #endregion
    public void ChangeAnimationState(string newAnimationState)
    {
        if (currentAnimationState == newAnimationState) return;
        animator.Play(newAnimationState);
        currentAnimationState = newAnimationState;
        //Debug.Log("Current Animation: " +  newAnimationState);
    }

    #region Attack, Hurt
    public void TakeDamage(int damage)
    {
        healthSystem.DamageTaken(damage);
        Debug.Log("Damage% dealt: " +  healthSystem.GetHealthPercent());
        stateMachine.ChangeState(EnemyStateID.Hurt);

    }

    public HealthSystem_Enemy HealthSystem
    {
        get { return healthSystem; }
        set { healthSystem = value; }
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }
    public void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, attackLayer);

        foreach (Collider2D hit in hits)
        {
            Player player = hit.GetComponentInParent<Player>();
            player.stateMachine.ChangeState(PlayerStateID.Hurt);
            Debug.Log("Player has been hit: " + hit.gameObject.name); // Optional debug
        }
    }

    public bool IsAttacking
    {
        get { return isAttacking; }
        set { isAttacking = value; }
    }
    #endregion

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    #region Flip
    public void FlipCharacter()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    void CheckRightOrLeft()
    {
        if (rb.velocity.x > 0 && !isFacingRight)
        {
            FlipCharacter();
        }
            
        else if (rb.velocity.x < 0 && isFacingRight)
        {
            FlipCharacter();
        }
    }
    #endregion

    #region Patrol
    public bool IsPatrolling
    {
        get { return isPatrolling; }
        set { isPatrolling = value; }
    }
    public bool IsFacingRight
    {
        get { return isFacingRight; }
        set { isFacingRight = value; }
    }
    public void StartPatrol()
    {
        stateMachine.ChangeState(EnemyStateID.Patrol);
    }
    public void Patrolling()
    {
        currentPointDirection = currentPoint.position.x - transform.position.x;
        if (currentPoint == pointB)
        {
            rb.velocity = new Vector2(Mathf.Sign(currentPointDirection) * moveSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Sign(currentPointDirection) * moveSpeed, 0);
        }

        if (currentPoint == pointA && Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            currentPoint = pointB;
        }

        if (currentPoint == pointB && Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            currentPoint = pointA;
        }
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(pointA.position, pointA_Radius);
        Gizmos.DrawWireSphere(pointB.position, pointB_Radius);

        Gizmos.DrawLine(pointA.position, pointB.position);

        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
    #endregion

    #region Chase
    void DetectPlayer()
    {
        float direction = transform.localScale.x > 0 ? 1 : -1;

        RaycastHit2D hit = Physics2D.Raycast(pointOfVision.position, Vector2.right * direction, visionDistance, playerLayer);
        Debug.DrawRay(pointOfVision.position, Vector2.right * direction * visionDistance, Color.red);


        if (hit.collider != null)
        {
            playerInSight = true;
            player = hit.collider.GetComponentInParent<Player>();
            directionToPlayer = player.transform.position.x - transform.position.x;
            if (Vector2.Distance(transform.position, player.transform.position) < 1f && !isAttacking && !player.IsDead)
            {
                //Debug.Log("Working");
                stateMachine.ChangeState(EnemyStateID.Attack);
            }
        }
        else
        {
            playerInSight = false;
        }
    }

    public bool PlayerInSight
    {
        get { return playerInSight; }
        set { playerInSight = value; }
    }
    public bool IsChasing
    {
        get { return isChasing; }
        set { isChasing = value; }
    }
    public void StartChasing()
    {
        stateMachine.ChangeState(EnemyStateID.Chase);
    }

    public void ChasePlayer()
    {
        rb.velocity = new Vector2(Mathf.Sign(directionToPlayer) * moveSpeed, 0);
    }

    #endregion

    public float KnockBackForce
    {
        get { return knockBackForce; }
        set { knockBackForce = value; }
    }

    public bool IsHurt
    {
        get { return isHurt; }
        set { isHurt = value; }
    }
}