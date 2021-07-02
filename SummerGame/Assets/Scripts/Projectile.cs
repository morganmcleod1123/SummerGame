using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float lifeTime;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("DestroyProjectile", lifeTime);
        animator.SetBool("Alive", true);
    }

    private void Update()
    {

    }

    void DestroyProjectile()
    {
        int deathAnim = Random.RandomRange(0, 2);
        if(deathAnim == 0)
        {
            animator.SetInteger("DeathAnim", 1);
        }
        if (deathAnim == 1)
        {
            animator.SetInteger("DeathAnim", 2);
        }
        Destroy(gameObject);
        animator.SetBool("Alive", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            //explode
            Destroy(gameObject);
        }
    }
}
