using UnityEngine;
using UnityEngine.InputSystem;

public class BroterMovementScript : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;

    public Rigidbody2D rb2d;

    private float _movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.linearVelocityX = _movement;
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<Vector2>().x * moveSpeed;
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        rb2d.linearVelocityY = jumpHeight;
    }
}
