using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : IPlayerState
{
    public PlayerStateID GetID()
    {
        return PlayerStateID.Death;
    }

    public void Enter(Player player)
    {
        player.IsDead = true;
        player.ChangeAnimationState(Player.Player_Death);
        //player.GetComponentInChildren<Collider2D>().enabled = false;
    }

    public void FixedUpdate(Player player)
    {
        
    }

    public void Update(Player player)
    {
        
    }

    public void Exit(Player player)
    {
        player.IsDead = false;
    }
}
