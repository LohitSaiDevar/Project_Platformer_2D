using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    public PlayerStateID GetID()
    {
        return PlayerStateID.Jump;
    }

    public void Enter(Player player)
    {
        //Debug.Log("Jumping State");
        player.IsJumping = true;
        player.ChangeAnimationState(Player.Player_Jump);
        player.rb.AddForce(Vector3.up * player.JumpForce, ForceMode2D.Impulse);
    }

    public void Update(Player player)
    {
        if (player.IsJumping && player.rb.velocity.y < 1)
        {
            player.stateMachine.ChangeState(PlayerStateID.Fall);
        }

        player.HandleWallSlide();
    }

    public void Exit(Player player)
    {
        player.IsJumping = false;
    }

    public void FixedUpdate(Player player)
    {
        player.MovePlayer();
    }
}
