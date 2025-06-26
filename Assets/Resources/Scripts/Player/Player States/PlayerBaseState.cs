using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public enum PlayerStateID
    {
        Idle,
        Run,
        Attack,
        Jump,
        WallJump,
        WallSlide,
        Hurt,
        Fall,
        Death
    }

    public interface IPlayerState
    {
        PlayerStateID GetID();
        void Enter(PlayerController player);
        void Update(PlayerController player);
        void FixedUpdate(PlayerController player);
        void Exit(PlayerController player);
    }

}