using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyChaseState : IEnemyState
    {
        public EnemyStateID GetID()
        {
            return EnemyStateID.Chase;
        }

        public void Enter(Enemy enemy)
        {
            enemy.IsChasing = true;
            enemy.ChangeAnimationState(Enemy.Enemy_Run);
        }

        public void Update(Enemy enemy)
        {
            if (!enemy.PlayerInSight && enemy is Grunt grunt)
            {
                grunt.StartPatrol();
            }

            if (enemy.player != null)
            {
                float playerX = enemy.player.transform.position.x;
                float enemyX = enemy.transform.position.x;
                enemy.directionToPlayer = Mathf.Sign(playerX - enemyX);
            }
        }

        public void Exit(Enemy enemy)
        {
            enemy.IsChasing = false;
        }

        public void FixedUpdate(Enemy enemy)
        {
            enemy.Chase();
        }
    }
}