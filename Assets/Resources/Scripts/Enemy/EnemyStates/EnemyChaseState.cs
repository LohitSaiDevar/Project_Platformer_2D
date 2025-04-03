using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : IEnemyState
{
    public EnemyStateID GetID()
    {
        return EnemyStateID.Chase;
    }

    public void Enter(Enemy enemy)
    {
        enemy.IsChasing = true;
    }

    public void Update(Enemy enemy)
    {
        enemy.ChangeAnimationState(Enemy.Enemy_Run);

    }

    public void Exit(Enemy enemy)
    {
        enemy.IsChasing = false;
    }

}
