using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    private float dazedTime;
    public float hitstun;

    private Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        dazedTime = 0;
    }

    void Update()
    {
        if(dazedTime > 0)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
        } 
        else
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.None;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        dazedTime -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        dazedTime = hitstun;
        health -= damage;
        Debug.Log("Took " + damage + " damage");
    }
}
