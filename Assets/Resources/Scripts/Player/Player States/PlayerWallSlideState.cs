using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Player
{
    public class PlayerWallSlideState : IPlayerState
    {
        float wallSlideTimer;
        public PlayerStateID GetID()
        {
            return PlayerStateID.WallSlide;
        }

        public void Enter(PlayerController player)
        {
            player.ChangeAnimationState(PlayerController.Player_WallSlide);
            player.IsWallSliding = true;
            player.wallJumpingDirection = -player.transform.localScale.x;
            wallSlideTimer = 0;
        }

        public void Update(PlayerController player)
        {
            //Debug.Log("VelocityY: " + player.rb.velocity.y);

            if (player.IsGrounded())
            {
                player.stateMachine.ChangeState(PlayerStateID.Idle);
            }
        }

        public void Exit(PlayerController player)
        {
            player.IsWallSliding = false;
        }

        public void FixedUpdate(PlayerController player)
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
        }
    }
}