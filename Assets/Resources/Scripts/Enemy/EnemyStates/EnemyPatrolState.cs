using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    public EnemyStateID GetID()
    {
        return EnemyStateID.Patrol;
    }

    public void Enter(Enemy enemy)
    {
        enemy.IsPatrolling = true;
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
        enemy.Patrolling();
    }

    public void Exit(Enemy enemy)
    {
        enemy.IsPatrolling = false;
    }

    
}
