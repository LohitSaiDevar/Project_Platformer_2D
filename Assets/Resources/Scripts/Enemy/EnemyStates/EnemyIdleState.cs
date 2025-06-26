using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyIdleState : IEnemyState
    {
        public EnemyStateID GetID()
        {
            return EnemyStateID.Idle;
        }
        public void Enter(Enemy enemy)
        {
            enemy.ChangeAnimationState(Enemy.Enemy_Idle);
            Debug.Log("Entered Idle State");
        }
        public void Update(Enemy enemy)
        {
            if (!enemy.IsGrounded())
            {

            }

            if (enemy.IsGrounded() && !enemy.PlayerInSight && enemy is Grunt grunt)
            {
                grunt.StartPatrol();
            }

            if (enemy.IsGrounded() && enemy.PlayerInSight)
            {
                enemy.StartChasing();
            }
        }
        public void Exit(Enemy enemy)
        {

        }

        public void FixedUpdate(Enemy enemy)
        {

        }
    }
}