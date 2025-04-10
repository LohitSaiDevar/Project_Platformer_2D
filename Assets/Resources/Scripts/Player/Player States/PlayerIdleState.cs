using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public PlayerStateID GetID()
    {
        return PlayerStateID.Idle;
    }

    public void Enter(Player player)
    {
        //ResetAllAnimations(player);
        player.ChangeAnimationState(Player.Player_Idle);
    }

    public void Update(Player player)
    {
        if (Mathf.Abs(player.HorizontalInput) > 0 && player.IsGrounded())
        {
            player.stateMachine.ChangeState(PlayerStateID.Run);
        }

        if (!player.IsGrounded() && player.rb.velocity.y < 0)
        {
            player.stateMachine.ChangeState(PlayerStateID.Fall);
        }
    }

    public void Exit(Player player)
    {
        player.animator.SetBool("isGrounded", false);
    }

    void ResetAllAnimations(Player player)
    {
        player.animator.SetBool("isGrounded", true);
        player.animator.SetFloat("VelocityY", 0);
        player.animator.SetFloat("MoveSpeed", 0);
        player.animator.SetBool("isWallSliding", false);
    }

    public void FixedUpdate(Player player)
    {
        
    }
}
