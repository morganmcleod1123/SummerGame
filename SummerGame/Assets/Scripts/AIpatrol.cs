using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIpatrol : MonoBehaviour
{

    public bool activePatrol;
    public bool activeChase;
    public Rigidbody2D rigidb;
    public float walkSpeed;
    public bool activeTurn;
    public Transform groundCheckPosition;
    public LayerMask wallLayer;
    public bool enemyFacingRight = true;
    public float startPos;
    public bool backAtStart = false;

    [SerializeField]
    Transform player;

    [SerializeField]
    float detectRange;

    public float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rigidb = GetComponent<Rigidbody2D>();
        activePatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        //print(transform.position.x);

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
            activePatrol = false;
            Chase();
            
        }
        else if (activeChase == true && playerDistance > detectRange)
        {
            //stop following
            StopChase();
        }

        //If enemy is not patrolling and doesn't detect player
        if (!activePatrol && !activeChase)
        {
            ReturnToStart();
        }
    }

    private void FixedUpdate()
    {
        if (activePatrol)
        {
            activeTurn = Physics2D.OverlapCircle(groundCheckPosition.position, 0.1f, wallLayer);
        }
        print(enemyFacingRight);
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
        enemyFacingRight = !enemyFacingRight;
    }

    void Chase()
    {
        activeChase = true;
        if(transform.position.x < player.position.x)
        {
            //enemy is left of the player
            if (!enemyFacingRight)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                enemyFacingRight = true;
                walkSpeed *= -1;
            }
            rigidb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rigidb.velocity.y);
        }
        else if (transform.position.x > player.position.x)
        {
            //enemy is right of the player
            if (enemyFacingRight)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                enemyFacingRight = false;
                walkSpeed *= -1;
            }
            rigidb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rigidb.velocity.y);
        }
    }

    void StopChase()
    {
        activeChase = false;
        rigidb.velocity = new Vector2(0, 0);
    }

    void ReturnToStart()
    {
        //Enemy is left of starting position
        if (transform.position.x < startPos - 0.2)
        {
            if (!enemyFacingRight)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                enemyFacingRight = true;
                walkSpeed *= -1;
            }
            rigidb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rigidb.velocity.y);
        }
        //Enemy is right of starting position
        else if (transform.position.x > startPos + 0.2)
        {
            if (enemyFacingRight)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                enemyFacingRight = false;
                walkSpeed *= -1;
            }
            rigidb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rigidb.velocity.y);
        }
        else
        {
            if (!enemyFacingRight)
            {
                Flip();
            }
            rigidb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rigidb.velocity.y);
            activePatrol = true;
        }
    }

    void ReturnFromLeft()
    {
            if (!enemyFacingRight)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                enemyFacingRight = true;
            }
            rigidb.velocity = new Vector2(followSpeed, 0);
        if (transform.position.x > startPos)
        {
            activePatrol = true;
        }
    }

    void ReturnFromRight()
    {
        while (transform.position.x > startPos)
        {
            if (enemyFacingRight)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                enemyFacingRight = false;
            }
            rigidb.velocity = new Vector2(-followSpeed, 0);
        }
    }
}
