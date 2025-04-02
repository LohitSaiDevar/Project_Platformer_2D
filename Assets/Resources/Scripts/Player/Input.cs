using UnityEngine;

public static class Input
{
    public static void Jump(Rigidbody2D rb, float jumpForce)
    {
        Debug.Log("Jump!");
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public static void Walk(Rigidbody2D rb, float walkSpeed)
    {
        Debug.Log("Walk");
    }
}
