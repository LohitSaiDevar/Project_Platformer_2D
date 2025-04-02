using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerWallSlideState : IPlayerState
{
    public PlayerStateID GetID()
    {
        return PlayerStateID.WallSlide;
    }

    public void Enter(Player player)
    {
        player.ChangeAnimationState(Player.Player_WallSlide);
        player.IsWallSliding = true;
        player.wallJumpingDirection = -player.transform.localScale.x;
    }

    public void Update(Player player)
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, Mathf.Clamp(player.rb.velocity.y, -player.wallSlidingSpeed, float.MaxValue));

        if (player.IsGrounded())
        {
            player.stateMachine.ChangeState(PlayerStateID.Idle);
        }
    }

    public void Exit(Player player)
    {
        player.IsWallSliding = false;
    }
}
