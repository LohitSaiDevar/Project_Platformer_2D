using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerRunState : IPlayerState
    {
        public PlayerStateID GetID()
        {
            return PlayerStateID.Run;
        }

        public void Enter(PlayerController player)
        {
            //Debug.Log("Run State!");
            player.IsRunning = true;
        }

        public void Update(PlayerController player)
        {
            player.ChangeAnimationState(PlayerController.Player_Run);
            if (Mathf.Abs(player.HorizontalInput) < 0.5f && player.IsGrounded())
            {
                player.stateMachine.ChangeState(PlayerStateID.Idle);
            }

            if (!player.IsGrounded() && player.rb.velocity.y < 0 && !player.IsJumping)
            {
                player.stateMachine.ChangeState(PlayerStateID.Fall);
            }
        }

        public void Exit(PlayerController player)
        {
            player.IsRunning = false;
        }

        public void FixedUpdate(PlayerController player)
        {
            player.MovePlayer();
        }
    }
}