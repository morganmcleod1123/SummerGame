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
    public bool enemyFacingRight = true;

    [SerializeField]
    Transform player;

    [SerializeField]
    float detectRange;

    public float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rigidb = GetComponent<Rigidbody2D>();
        activePatrol = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activePatrol)
        {
            Patrol();
        }

        //check distance from player
        float playerDistance = Vector2.Distance(transform.position, player.position);
        //print("distance to player " + playerDistance);
        if (playerDistance < detectRange)
        {
            //detection and follow
            Chase();
        }
        else
        {
            //stop following
            StopChase();
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

    void Chase()
    {

        if(transform.position.x < player.position.x)
        {
            //enemy is left of the player
            rigidb.velocity = new Vector2(followSpeed, 0);
            if (!enemyFacingRight)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                enemyFacingRight = true;
            }
        }
        else if (transform.position.x > player.position.x)
        {
            //enemy is right of the player
            rigidb.velocity = new Vector2(-followSpeed, 0);
            if (enemyFacingRight)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                enemyFacingRight = false;
            }
        }
    }

    void StopChase()
    {
        rigidb.velocity = new Vector2(0, 0);
    }
}
