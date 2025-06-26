using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerIdleState : IPlayerState
    {
        public PlayerStateID GetID()
        {
            return PlayerStateID.Idle;
        }

        public void Enter(PlayerController player)
        {
            //ResetAllAnimations(player);
            player.ChangeAnimationState(PlayerController.Player_Idle);
            player.rb.velocity = new Vector2(0, 0);
        }

        public void Update(PlayerController player)
        {
            if (Mathf.Abs(player.HorizontalInput) > 0 && player.IsGrounded() && !player.IsJumping)
            {
                player.stateMachine.ChangeState(PlayerStateID.Run);
            }

            if (!player.IsGrounded() && player.rb.velocity.y < 0)
            {
                player.stateMachine.ChangeState(PlayerStateID.Fall);
            }
        }

        public void Exit(PlayerController player)
        {
            player.animator.SetBool("isGrounded", false);
        }

        void ResetAllAnimations(PlayerController player)
        {
            player.animator.SetBool("isGrounded", true);
            player.animator.SetFloat("VelocityY", 0);
            player.animator.SetFloat("MoveSpeed", 0);
            player.animator.SetBool("isWallSliding", false);
        }

        public void FixedUpdate(PlayerController player)
        {

        }
    }
}