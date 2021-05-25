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

    void Start()
    {
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
        health -= damage;
        Debug.Log("Took " + damage + " damage");
    }
}
