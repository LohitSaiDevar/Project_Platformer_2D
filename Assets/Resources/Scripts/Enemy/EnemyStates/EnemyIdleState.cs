using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    public EnemyStateID GetID()
    {
        return EnemyStateID.Idle;
    }
    public void Enter(Enemy enemy)
    {
        enemy.ChangeAnimationState(Enemy.Enemy_Idle);
    }
    public void Update(Enemy enemy)
    {
        if (enemy.IsGrounded() && !enemy.PlayerInSight)
        {
            enemy.StartPatrol();
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
