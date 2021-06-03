using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public float lifeTime;
    private Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = transform.right * projectileSpeed;
        Invoke("DestroyProjectile", lifeTime);
    }

    void DestroyProjectile()
    {
        //Destroy Particle Effects
        Destroy(gameObject);
    }
}
