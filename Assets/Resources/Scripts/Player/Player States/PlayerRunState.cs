using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : IPlayerState
{
    public PlayerStateID GetID()
    {
        return PlayerStateID.Run;
    }

    public void Enter(Player player)
    {
        //Debug.Log("Run State!");
        player.IsRunning = true;
    }

    public void Update(Player player)
    {
        player.ChangeAnimationState(Player.Player_Run);
        if (Mathf.Abs(player.HorizontalInput) < 0.5f && player.IsGrounded())
        {
            player.stateMachine.ChangeState(PlayerStateID.Idle);
        }

        if (!player.IsGrounded() && player.rb.velocity.y < 0)
        {
            player.stateMachine.ChangeState(PlayerStateID.Fall);
        }
    }

    public void Exit(Player player)
    {
        player.IsRunning = false;
    }

    public void FixedUpdate(Player player)
    {
        player.MovePlayer();
    }
}
