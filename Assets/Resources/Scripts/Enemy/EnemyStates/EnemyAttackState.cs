using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyAttackState : IEnemyState
    {
        public EnemyStateID GetID()
        {
            return EnemyStateID.Attack;
        }
        public void Enter(Enemy enemy)
        {
            enemy.IsAttacking = true;
            enemy.ChangeAnimationState(Enemy.Enemy_Attack);

        }
        public void Update(Enemy enemy)
        {
            if (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                enemy.stateMachine.ChangeState(EnemyStateID.Idle);
            }
        }
        public void Exit(Enemy enemy)
        {
            enemy.IsAttacking = false;
        }

        public void FixedUpdate(Enemy enemy)
        {

        }
    }
}