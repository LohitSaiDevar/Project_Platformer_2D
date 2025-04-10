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
    Chase,
    Death
}

public interface IEnemyState
{
    EnemyStateID GetID();
    void Enter(Enemy enemy);
    void Update(Enemy enemy);
    void FixedUpdate(Enemy enemy);
    void Exit(Enemy enemy);
}
