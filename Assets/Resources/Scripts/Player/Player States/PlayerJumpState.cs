using UnityEngine;

namespace Player
{
    public class PlayerJumpState : IPlayerState
    {
        public PlayerStateID GetID()
        {
            return PlayerStateID.Jump;
        }

        public void Enter(PlayerController player)
        {
            //Debug.Log("Jumping State");
            player.IsJumping = true;
            player.ChangeAnimationState(PlayerController.Player_Jump);
            player.rb.AddForce(Vector3.up * player.JumpForce, ForceMode2D.Impulse);
        }

        public void Update(PlayerController player)
        {
            if (player.IsJumping && player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                player.stateMachine.ChangeState(PlayerStateID.Fall);
            }

            player.HandleWallSlide();
        }

        public void Exit(PlayerController player)
        {
            player.IsJumping = false;
        }

        public void FixedUpdate(PlayerController player)
        {
            player.MovePlayer();
        }
    }
}