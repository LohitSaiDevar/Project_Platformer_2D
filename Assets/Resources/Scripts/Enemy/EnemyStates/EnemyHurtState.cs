using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyHurtState : IEnemyState
    {
        bool isNotHurt;
        public EnemyStateID GetID()
        {
            return EnemyStateID.Hurt;
        }
        public void Enter(Enemy enemy)
        {
            enemy.IsHurt = true;
            enemy.ChangeAnimationState(Enemy.Enemy_Hurt);
            enemy.HurtToIdle();
        }
        public void Update(Enemy enemy)
        {
            if (enemy.HealthSystem.GetHealthPercent() <= 0)
            {
                enemy.stateMachine.ChangeState(EnemyStateID.Death);
            }
            else if (isNotHurt)
            {
                enemy.stateMachine.ChangeState(EnemyStateID.Idle);
                Debug.Log("changed to idle");
            }
            else if (enemy.PlayerInSight)
            {
                enemy.stateMachine.ChangeState(EnemyStateID.Chase);
            }
        }
        public void Exit(Enemy enemy)
        {
            enemy.IsHurt = false;
            //enemy.StartCoroutine(HurtDelay(enemy));
        }

        public void FixedUpdate(Enemy enemy)
        {

        }
    }
}