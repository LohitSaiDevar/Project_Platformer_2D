using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyPatrolState : IEnemyState
    {
        Grunt grunt;
        public EnemyStateID GetID()
        {
            return EnemyStateID.Patrol;
        }

        public void Enter(Enemy enemy)
        {
            if (enemy is Grunt grunt)
            {
                grunt.IsPatrolling = true;
            }
            //Debug.Log("Started Patrol");
        }

        public void Update(Enemy enemy)
        {
            enemy.ChangeAnimationState(Enemy.Enemy_Run);

            if (enemy.PlayerInSight)
            {
                enemy.StartChasing();
            }
        }

        public void FixedUpdate(Enemy enemy)
        {
            if (enemy is Grunt grunt)
            {
                grunt.Patrolling();
                grunt.DetectPlayer();
            }
        }

        public void Exit(Enemy enemy)
        {
            if (enemy is Grunt grunt)
            {
                grunt.IsPatrolling = false;
            }
        }
    }
}