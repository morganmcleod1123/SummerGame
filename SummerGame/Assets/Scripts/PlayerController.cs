using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private float moveInput;

    private bool facingRight = true;

    public bool canMove = true;
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public float jumpForce;
    public int extraJumps;
    private int currentJumps;

    private PlayerAbilities playerAbilities;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentJumps = extraJumps;
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAbilities = GetComponent<PlayerAbilities>();
    }

    void Update()
    {
        animator.SetBool("FacingRight", facingRight);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("CanMove", canMove);
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
        if (canMove)
        {
            animator.SetFloat("Horizontal", moveInput);
            animator.SetFloat("Vertical", rigidbody2D.velocity.y);
            if (Input.GetKeyDown(KeyCode.Space) && currentJumps > 0)
            {
                rigidbody2D.velocity = Vector2.up * jumpForce;
                currentJumps--;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && currentJumps == 0 && isGrounded)
            {
                rigidbody2D.velocity = Vector2.up * jumpForce;
            }
        } else
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
        }
    }

    private void FixedUpdate()
    {
        if (!playerAbilities.isDashing)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

            moveInput = Input.GetAxisRaw("Horizontal");
            if (canMove) { rigidbody2D.velocity = new Vector2(moveInput * moveSpeed, rigidbody2D.velocity.y); }
            else
            {
                rigidbody2D.velocity = new Vector2(0, 0);
            }
            if (!facingRight && moveInput > 0 && canMove)
            {
                Flip();
            }
            else if (facingRight && moveInput < 0 && canMove)
            {
                Flip();
            }
        }
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
