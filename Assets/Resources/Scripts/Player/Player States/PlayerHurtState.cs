using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : IPlayerState
{
    public PlayerStateID GetID()
    {
        return PlayerStateID.Hurt;
    }

    public void Enter(Player player)
    {
        player.IsHurt = true;
        Debug.Log("Animation is playing");
        player.ChangeAnimationState(Player.Player_Hurt);

        GameObject hp = player.HealthPoints[player.HealthPoints.Count - 1];
        player.HealthPoints.RemoveAt(player.HealthPoints.Count - 1);
        Player.Destroy(hp);
    }

    public void Update(Player player)
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            player.stateMachine.ChangeState(PlayerStateID.Idle);
        }

        if (player.HealthPoints.Count == 0)
        {
            player.stateMachine.ChangeState(PlayerStateID.Death);
        }
    }

    public void Exit(Player player)
    {
        player.IsHurt = false;
    }

    public void FixedUpdate(Player player)
    {
        
    }
}

