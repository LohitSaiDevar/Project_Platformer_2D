using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AnimationTrigger_Player : MonoBehaviour
    {
        [SerializeField] PlayerController player;
        public void OnAnimationTrigger_Attack()
        {
            player.stateMachine.ChangeState(PlayerStateID.Idle);
        }
    }
}