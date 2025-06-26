using Player;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Enemies
{
    public class Grunt : Enemy, IPatrol, IDetect
    {
        
        [Header("Chase Player Settings")]
        public Transform pointOfVision;
        
        public float visionDistance;
        public LayerMask playerLayer;
        [SerializeField] EnemyData enemyData;

        protected override void Awake()
        {
            base.Awake();
            currentPoint = pointA;
            healthSystem = new HealthSystem_Enemy(enemyData.MaxHP);
        }
        protected override void Update()
        {
            base.Update();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            
        }

        protected override void RegisterAllStates()
        {
            base.RegisterAllStates();
            stateMachine.RegisterState(new EnemyPatrolState());
        }

        public void DetectPlayer()
        {
            float direction = transform.localScale.x > 0 ? 1 : -1;

            RaycastHit2D hit = Physics2D.Raycast(pointOfVision.position, Vector2.right * direction, visionDistance, playerLayer);
            Debug.DrawRay(pointOfVision.position, Vector2.right * direction * visionDistance, UnityEngine.Color.red);


            if (hit.collider != null)
            {
                PlayerInSight = true;
                player = hit.collider.GetComponentInParent<PlayerController>();
                directionToPlayer = player.transform.position.x - transform.position.x;
                if (Vector2.Distance(transform.position, player.transform.position) < 1f && !IsAttacking && !player.IsDead)
                {
                    //Debug.Log("Working");
                    Attack();
                }
            }
            else
            {
                PlayerInSight = false;
            }
        }

        #region Patrol

        [Header("Patrol Settings")]
        public Transform pointA;
        public float pointA_Radius;
        public Transform pointB;
        public float pointB_Radius;
        Transform currentPoint;
        public float currentPointDirection;
        bool isPatrolling;
        [SerializeField] bool startPatrol;
        private bool movingRight = true;
        private float patrolDirection = 1;

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
            Move(patrolDirection);

            // If no ground detected or a wall is hit, change direction
            if (!IsGroundAhead() || IsWalled())
            {
                ChangeDirection();
            }
        }

        protected void ChangeDirection()
        {
            patrolDirection *= -1;
        }
        #endregion

        #region Search


        public IEnumerator SearchPlayer()
        {
            while (true)
            {

            }
        }
        #endregion
        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.DrawWireSphere(pointA.position, pointA_Radius);
            Gizmos.DrawWireSphere(pointB.position, pointB_Radius);

            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}