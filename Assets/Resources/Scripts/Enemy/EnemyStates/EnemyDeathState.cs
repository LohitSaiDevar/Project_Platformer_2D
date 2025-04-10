using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : IEnemyState
{
    public EnemyStateID GetID()
    {
        return EnemyStateID.Death;
    }

    public void Enter(Enemy enemy)
    {
        enemy.IsDead = true;
        enemy.ChangeAnimationState(Enemy.Enemy_Death);
    }
    public void Update(Enemy enemy)
    {
        if (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            enemy.StartCoroutine(DestroyDelay(enemy));
        }
    }

    public void FixedUpdate(Enemy enemy)
    {
        
    }

    public void Exit(Enemy enemy)
    {
        enemy.IsDead = false;
    }

    IEnumerator DestroyDelay(Enemy enemy)
    {
        yield return new WaitForSeconds(1);
        Enemy.Destroy(enemy.gameObject);
    }
}
