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
        enemy.IsHurt = true;
        enemy.ChangeAnimationState(Enemy.Enemy_Hurt);

        float knockBackDirection = enemy.IsFacingRight ? -1 : 1;
        float knockBackForce = enemy.KnockBackForce;

        enemy.rb.velocity = Vector2.zero; // Optional, only if needed
        enemy.rb.AddForce(Vector2.right * knockBackDirection * knockBackForce, ForceMode2D.Impulse);
    }
    public void Update(Enemy enemy)
    {
        if (enemy.HealthSystem.GetHealthPercent() <= 0)
        {
            enemy.stateMachine.ChangeState(EnemyStateID.Death);
        }
        else
        {
            if (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                enemy.stateMachine.ChangeState(EnemyStateID.Idle);
            }
        }
    }
    public void Exit(Enemy enemy)
    {
        enemy.StartCoroutine(HurtDelay(enemy));
    }

    IEnumerator HurtDelay(Enemy enemy)
    {
        yield return new WaitForSeconds(1);
        enemy.IsHurt = false;
    }

    public void FixedUpdate(Enemy enemy)
    {
        
    }
}
