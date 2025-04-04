using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerWallSlideState : IPlayerState
{
    float wallSlideTimer;
    public PlayerStateID GetID()
    {
        return PlayerStateID.WallSlide;
    }

    public void Enter(Player player)
    {
        player.ChangeAnimationState(Player.Player_WallSlide);
        player.IsWallSliding = true;
        player.wallJumpingDirection = -player.transform.localScale.x;
        wallSlideTimer = 0;
    }

    public void Update(Player player)
    {
        wallSlideTimer += Time.deltaTime;
        float fallSpeed;
        if (wallSlideTimer < 2)
        {
            fallSpeed = -player.wallSlidingSpeed; // Constant fall speed for first 2 seconds
        }
        else
        {
            //Debug.Log("WallSlideTimer: " + wallSlideTimer);
            float acceleration = (wallSlideTimer - 2f) * 5; // Slow acceleration over time
            float maxFallSpeed = -15f;
            fallSpeed = Mathf.Max(-player.wallSlidingSpeed - acceleration, maxFallSpeed);
        }

        player.rb.velocity = new Vector2(player.rb.velocity.x, fallSpeed);
        //Debug.Log("VelocityY: " + player.rb.velocity.y);

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
