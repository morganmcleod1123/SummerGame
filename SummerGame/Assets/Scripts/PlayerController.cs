using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private float moveInput;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public float jumpForce;
    public int extraJumps;
    private int currentJumps;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;
    Animator animator;
    private float timePassed;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentJumps = extraJumps;
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timePassed = 0;
    }

    void Update()
    {
        animator.SetBool("FacingRight", facingRight);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("Horizontal", moveInput);
        animator.SetFloat("Vertical", rigidbody2D.velocity.y);
        if (!facingRight)
        {
            spriteRenderer.flipX = true;
        } else
        {
            spriteRenderer.flipX = false;
        }
        if(isGrounded)
        {
            currentJumps = extraJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space) && currentJumps > 0)
        {
            rigidbody2D.velocity = Vector2.up * jumpForce;
            currentJumps--;
        } else if (Input.GetKeyDown(KeyCode.Space) && currentJumps == 0 && isGrounded)
        {
            rigidbody2D.velocity = Vector2.up * jumpForce;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxisRaw("Horizontal");
        rigidbody2D.velocity = new Vector2(moveInput * moveSpeed, rigidbody2D.velocity.y);

        if (!facingRight && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight && moveInput < 0)
        {
            Flip();
        }
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
