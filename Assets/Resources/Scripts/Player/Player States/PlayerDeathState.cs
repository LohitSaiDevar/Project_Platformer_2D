using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerDeathState : IPlayerState
    {
        public PlayerStateID GetID()
        {
            return PlayerStateID.Death;
        }

        public void Enter(PlayerController player)
        {
            player.IsDead = true;
            player.ChangeAnimationState(PlayerController.Player_Death);
            //player.GetComponentInChildren<Collider2D>().enabled = false;
        }

        public void FixedUpdate(PlayerController player)
        {

        }

        public void Update(PlayerController player)
        {

        }

        public void Exit(PlayerController player)
        {
            player.IsDead = false;
        }
    }

}