using UnityEngine;
using UnityEngine.InputSystem;

public class BroterMovementScript : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;

    private Rigidbody2D rb2d;

    private float _movement;

    bool grounded;

   //SerializeField] private Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() 
    {
        rb2d.linearVelocityX = _movement;

        if (_movement > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if (_movement < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<Vector2>().x * moveSpeed;
       //f (_movement != 0)
       //
        //  _animator.SetBool("isRunning", true);
       //
       //else
      //{
      //    _animator.SetBool("isRunning", false);
      //}
    }

    public void Jump(InputAction.CallbackContext ctx) 
    {
        if (ctx.ReadValue<float>()== 1 && grounded) 
            {
                rb2d.linearVelocityY = jumpHeight;
            }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            Vector3 normal = other.GetContact(0).normal;
            if (normal == Vector3.up)
            {
                grounded = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
}
