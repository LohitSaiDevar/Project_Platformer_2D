using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : IPlayerState
{
    public PlayerStateID GetID()
    {
        return PlayerStateID.Fall;
    }

    public void Enter(Player player)
    {
        player.IsFalling = true;
    }

    public void Update(Player player)
    {
        
        player.ChangeAnimationState(Player.Player_Fall);
        //Debug.Log("Is falling");
        if (player.IsGrounded())
        {
            player.stateMachine.ChangeState(PlayerStateID.Idle);
        }
        player.HandleWallSlide();
    }

    public void Exit(Player player)
    {
        player.IsFalling = false;
    }
}
