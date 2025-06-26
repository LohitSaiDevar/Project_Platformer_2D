using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAttackState : IPlayerState
    {
        public PlayerStateID GetID()
        {
            return PlayerStateID.Attack;
        }

        public void Enter(PlayerController player)
        {
            player.ChangeAnimationState(PlayerController.Player_Attack);
            //Debug.Log("player attacks");
            player.DealDamage();

        }

        public void Update(PlayerController player)
        {
            if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                player.stateMachine.ChangeState(PlayerStateID.Idle);
            }
        }

        public void Exit(PlayerController player)
        {

        }

        public void FixedUpdate(PlayerController player)
        {

        }
    }
}