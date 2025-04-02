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

    [Header("Movement Settings")]
    bool isPatrolling;
    [SerializeField] bool startPatrol;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform pointA;
    [SerializeField] float pointA_Radius;
    [SerializeField] Transform pointB;
    [SerializeField] float pointB_Radius;
    Transform currentPoint;

    public EnemyStateMachine stateMachine;
    [SerializeField] EnemyStateID initialState;

    [Header("Chase Player Settings")]
    Transform pointOfVision;
    bool playerInSight = false;
    [SerializeField] float visionDistance;
    [SerializeField] LayerMask playerLayer;

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
        stateMachine.ChangeState(initialState);

        currentPoint = pointA;
    }

    private void FixedUpdate()
    {
        if (isPatrolling)
        {
            Patrolling();
        }
    }
    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        if (startPatrol)
        {
            stateMachine.ChangeState(EnemyStateID.Patrol);
        }
        else
        {
            stateMachine.ChangeState(EnemyStateID.Idle);
        }

        CheckRightOrLeft();
    }

    #endregion
    public void ChangeAnimationState(string newAnimationState)
    {
        if (currentAnimationState == newAnimationState) return;
        animator.Play(newAnimationState);
        currentAnimationState = newAnimationState;
        //Debug.Log("Current Animation: " +  newAnimationState);
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("Damage dealt: " +  damage);
        ChangeAnimationState(Enemy_Hurt);
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
            FlipCharacter();
        else if (rb.velocity.x < 0 && isFacingRight)
            FlipCharacter();
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
    void Patrolling()
    {
        if (currentPoint == pointB)
        {
            rb.velocity = new Vector2 (moveSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2 (-moveSpeed, 0);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(pointA.position, pointA_Radius);
        Gizmos.DrawWireSphere(pointB.position, pointB_Radius);

        Gizmos.DrawLine(pointA.position, pointB.position);
    }
    #endregion

    void LookPlayer()
    {
        Ray ray = new Ray();
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, visionDistance, playerLayer))
        {

        }
    }
}
