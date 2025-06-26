using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerHurtState : IPlayerState
    {
        public PlayerStateID GetID()
        {
            return PlayerStateID.Hurt;
        }

        public void Enter(PlayerController player)
        {
            player.IsHurt = true;
            Debug.Log("Animation is playing");
            player.ChangeAnimationState(PlayerController.Player_Hurt);

            GameObject hp = player.HealthPoints[player.HealthPoints.Count - 1];
            player.HealthPoints.RemoveAt(player.HealthPoints.Count - 1);
            PlayerController.Destroy(hp);
        }

        public void Update(PlayerController player)
        {
            if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                player.stateMachine.ChangeState(PlayerStateID.Idle);
            }

            if (player.HealthPoints.Count == 0)
            {
                player.stateMachine.ChangeState(PlayerStateID.Death);
            }
        }

        public void Exit(PlayerController player)
        {
            player.IsHurt = false;
        }

        public void FixedUpdate(PlayerController player)
        {

        }
    }
}
