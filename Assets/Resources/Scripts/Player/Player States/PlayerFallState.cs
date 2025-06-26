using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerFallState : IPlayerState
    {
        public PlayerStateID GetID()
        {
            return PlayerStateID.Fall;
        }

        public void Enter(PlayerController player)
        {
            player.IsFalling = true;
        }

        public void Update(PlayerController player)
        {

            player.ChangeAnimationState(PlayerController.Player_Fall);
            //Debug.Log("Is falling");
            if (player.IsGrounded())
            {
                player.stateMachine.ChangeState(PlayerStateID.Idle);
            }
            player.HandleWallSlide();
        }

        public void Exit(PlayerController player)
        {
            player.IsFalling = false;
        }

        public void FixedUpdate(PlayerController player)
        {
            player.MovePlayer();
        }
    }
}