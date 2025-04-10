using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IPlayerState
{
    public PlayerStateID GetID()
    {
        return PlayerStateID.Attack;
    }

    public void Enter(Player player)
    {
        player.ChangeAnimationState(Player.Player_Attack);
        //Debug.Log("player attacks");
        player.DealDamage();

    }

    public void Update(Player player)
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            player.stateMachine.ChangeState(PlayerStateID.Idle);
        }
    }

    public void Exit(Player player)
    {
        
    }

    public void FixedUpdate(Player player)
    {
        
    }
}
