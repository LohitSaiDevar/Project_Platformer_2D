using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateID
{
    Idle,
    Run,
    Attack,
    Hurt,
    Patrol,
    Chase
}

public interface IEnemyState
{
    EnemyStateID GetID();
    void Enter(Enemy enemy);
    void Update(Enemy enemy);
    void Exit(Enemy enemy);
}
