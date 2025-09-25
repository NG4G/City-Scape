using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;

public class BroterMovementScript : MonoBehaviour
{
    float horizontalInput;
    public float moveSpeed = -5f;
    bool isFacingRight;
    public float jumpPower = 15f;
    bool isGrounded = false;

    Rigidbody2D rb;
    Animator animator;

    public GameObject attackPoint;
    public float radius;
    public LayerMask enemies;
    public float damage;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    private float dashingTime = 0.35f;
    private float dashingCooldown = 1f;

    [SerializeField] private TrailRenderer tr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() 
    {
        if (isDashing)
        {
            return;
        }

        horizontalInput = Input.GetAxis("Horizontal");

        FlipSprite();

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }

        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isAttacking", true);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    public void attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        foreach (Collider2D enemyGameObject in enemy)
        {
            enemyGameObject.GetComponent<EnemyHealth>().health -= damage;
        }
    }

    void endAttack()
    {
        animator.SetBool("isAttacking", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);

    }

    void FlipSprite()
    {
        if(isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            Vector3 ls = transform.localScale;
            isFacingRight = !isFacingRight;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }
    

    void OnTriggerEnter2D(Collider2D collsion)
    {
        isGrounded = true;
        animator.SetBool("isJumping", !isGrounded);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
