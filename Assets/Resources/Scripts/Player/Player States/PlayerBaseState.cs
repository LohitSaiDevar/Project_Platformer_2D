using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Enter(Player player);
    void Update(Player player);
    void FixedUpdate(Player player);
    void Exit(Player player);
}
