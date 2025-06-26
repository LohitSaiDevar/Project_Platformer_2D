using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Player
{
    public class PlayerWallJumpState : IPlayerState
    {
        public PlayerStateID GetID()
        {
            return PlayerStateID.WallJump;
        }

        public void Enter(PlayerController player)
        {
            player.StartWallJump = false;
            player.IsWallJumping = true;
            player.ChangeAnimationState(PlayerController.Player_Jump);
            player.rb.velocity = new Vector2(player.wallJumpingDirection * player.wallJumpingPower.x, player.wallJumpingPower.y);
            if ((player.wallJumpingDirection > 0 && !player.IsFacingRight) ||
                (player.wallJumpingDirection < 0 && player.IsFacingRight))
            {
                player.FlipCharacter_Alt();
            }
            // Apply jump force once instead of every frame
        }

        public void Update(PlayerController player)
        {

            if (player.rb.velocity.y < 0)
            {
                player.stateMachine.ChangeState(PlayerStateID.Fall);
            }
        }

        public void Exit(PlayerController player)
        {
            player.IsWallJumping = false;
        }

        public void FixedUpdate(PlayerController player)
        {

        }
    }
}