using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Player;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IDamageable, IChaseable
    {
        public PlayerController player;
        public Rigidbody2D rb;
        public bool isFacingRight = true;

        public EnemyStateMachine stateMachine;

        public float moveSpeed;
        bool playerInSight = false;
        bool isChasing;

        [Header("Checks")]
        [SerializeField] protected Transform groundCheck;
        [SerializeField] protected Transform groundCheckAhead;
        [SerializeField] protected Transform wallCheck;
        [SerializeField] protected LayerMask groundLayer;
        [SerializeField] protected LayerMask wallLayer;
        [SerializeField] protected float wallDetectionDistance = 0.5f; // How far to check for walls ahead

        [Header("Health System")]
        public HealthSystem_Enemy healthSystem;
        
        bool isDead;

        #region Start, Update, FixedUpdate, Register States
        protected virtual void Awake()
        {
            RegisterAllStates();
        }
        // Update is called once per frame
        protected virtual void Update()
        {
            stateMachine.Update();
        }

        protected virtual void FixedUpdate()
        {
            stateMachine.FixedUpdate();
            
        }

        protected virtual void RegisterAllStates()
        {
            stateMachine = new EnemyStateMachine(this);
            stateMachine.RegisterState(new EnemyIdleState());
            stateMachine.RegisterState(new EnemyRunState());
            stateMachine.RegisterState(new EnemyChaseState());
            stateMachine.RegisterState(new EnemyAttackState());
            stateMachine.RegisterState(new EnemyHurtState());
            stateMachine.RegisterState(new EnemyDeathState());
            stateMachine.ChangeState(initialState);
        }
        #endregion

        #region OnEnable, OnDisable

        private void OnEnable()
        {
            GameEvents.OnPlayerAttack += TakeDamage;
        }
        private void OnDisable()
        {
            GameEvents.OnPlayerAttack -= TakeDamage;
        }

        #endregion

        #region Animation

        //Animation settings
        public Animator animator;
        public string currentAnimationState;
        [SerializeField] EnemyStateID initialState;
        public const string Enemy_Idle = "Enemy_Idle";
        public const string Enemy_Run = "Enemy_Run";
        public const string Enemy_Attack = "Enemy_Attack";
        public const string Enemy_Hurt = "Enemy_Hurt";
        public const string Enemy_Fall = "Enemy_Fall";
        public const string Enemy_Death = "Enemy_Death";

        public void ChangeAnimationState(string newAnimationState)
        {
            if (currentAnimationState == newAnimationState) return;
            animator.Play(newAnimationState);
            currentAnimationState = newAnimationState;
            Debug.Log("Current Animation: " +  newAnimationState);
        }
        
        #endregion

        #region Attack, Hurt

        [Header("Attack Settings")]
        public Transform attackPoint;
        public float attackRange;
        public LayerMask attackLayer;
        private bool isAttacking;
        public float directionToPlayer;

        public float knockBackForce;
        Vector2 knockBackDirection;
        bool isHurt;
        public void Attack()
        {
            stateMachine.ChangeState(EnemyStateID.Attack);
        }

        public void DealDamage()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, attackLayer);

            foreach (Collider2D hit in hits)
            {
                GameEvents.OnEnemyAttack();
                Debug.Log("Player has been hit: " + hit.gameObject.name); // Optional debug
            }
        }

        public void TakeDamage(int dmg, Vector3 playerPos)
        {
            directionToPlayer = Mathf.Sign(playerPos.x - transform.position.x);
            playerInSight = true;

            stateMachine.ChangeState(EnemyStateID.Hurt);
            healthSystem.DamageTaken(dmg);
        }

        public void HurtToIdle()
        {
            StartCoroutine(HurtToIdleCoroutine());
        }

        private IEnumerator HurtToIdleCoroutine()
        {
            ChangeAnimationState(Enemy_Hurt);
            yield return new WaitForSeconds(0.5f);

            if (HealthSystem.GetHealthPercent() <= 0)
            {
                stateMachine.ChangeState(EnemyStateID.Death);
            }
            else
            {
                stateMachine.ChangeState(EnemyStateID.Idle);
            }
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


        public bool IsAttacking
        {
            get { return isAttacking; }
            set { isAttacking = value; }
        }
        #endregion

        public virtual bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        }
        public virtual bool IsGroundAhead()
        {
            return Physics2D.OverlapCircle(groundCheckAhead.position, 0.2f, groundLayer);
        }

        protected bool IsWalled()
        {
            return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
        }
        #region Flip


        public void FlipCharacter()
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        public void FaceDirection(float direction)
        {
            if ((direction > 0 && !isFacingRight) || (direction < 0 && isFacingRight))
            {
                FlipCharacter();
            }
        }
        #endregion
        #region Gizmos
        protected virtual void OnDrawGizmosSelected()
        {
            if (attackPoint != null)
            {
                Gizmos.DrawWireSphere(attackPoint.position, attackRange);
            }
        }
        #endregion

        #region Move and Chase


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
        
        public void Move(float direction)
        {
            FaceDirection(direction);
            rb.velocity = new Vector2(direction * moveSpeed, 0);
        }
        public virtual void Chase()
        {
            Move(Mathf.Sign(directionToPlayer));
        }

        #endregion

        public float KnockBackForce
        {
            get { return knockBackForce; }
            set { knockBackForce = value; }
        }

        public Vector2 KnockBackDirection
        {
            get { return knockBackDirection; }
            set { knockBackDirection = value; }
        }

        public bool IsHurt
        {
            get { return isHurt; }
            set { isHurt = value; }
        }
    }
}