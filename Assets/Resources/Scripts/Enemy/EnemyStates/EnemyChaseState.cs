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
        
    }

    public void Update(Enemy enemy)
    {

    }

    public void Exit(Enemy enemy)
    {
        
    }

}
