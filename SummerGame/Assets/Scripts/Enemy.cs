using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    private float speed;
    public float startSpeed;
    private float dazedTime;
    public float hitstun;

    private Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        speed = startSpeed;
    }

    void Update()
    {
        if(dazedTime <= 0)
        {
            speed = startSpeed;
        } else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        dazedTime = hitstun;
        //rigidbody2D.AddForce();
        health -= damage;
        Debug.Log("Took " + damage + " damage");
    }
}
