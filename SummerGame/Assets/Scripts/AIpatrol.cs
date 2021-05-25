using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIpatrol : MonoBehaviour
{

    public bool activePatrol;
    public Rigidbody2D rigidb;
    public float walkSpeed;
    public bool activeTurn;
    public Transform groundCheckPosition;
    public LayerMask groundLayer; 

    // Start is called before the first frame update
    void Start()
    {
        activePatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (activePatrol)
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if (activePatrol)
        {
            activeTurn = !Physics2D.OverlapCircle(groundCheckPosition.position, 0.1f, groundLayer);
        }
    }

    void Patrol()
    {
        if (activeTurn)
        {
            Flip();
        }
        rigidb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rigidb.velocity.y);
    }

    void Flip()
    {
        activePatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        activePatrol = true;
    }
}
