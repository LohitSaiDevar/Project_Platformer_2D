using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : IEnemyState
{
    public EnemyStateID GetID()
    {
        return EnemyStateID.Hurt;
    }
    public void Enter(Enemy enemy)
    {
        enemy.TakeDamage(10);
        enemy.ChangeAnimationState(Enemy.Enemy_Hurt);
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

    }
}
