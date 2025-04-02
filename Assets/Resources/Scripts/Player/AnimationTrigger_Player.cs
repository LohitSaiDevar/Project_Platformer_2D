using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger_Player : MonoBehaviour
{
    [SerializeField] Player player;
    public void OnAnimationTrigger_Attack()
    {
        player.stateMachine.ChangeState(PlayerStateID.Idle);
    }
}
