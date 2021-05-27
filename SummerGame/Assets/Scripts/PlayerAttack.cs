using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implementation Tutorial https://youtu.be/1QfxdUpVh5I
public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPosition;
    public LayerMask whatIsEnemy;
    public float attackHitboxSize;
    public int damage;

    private void Start()
    {
        timeBtwAttack = startTimeBtwAttack;
    }
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Shwing!");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackHitboxSize, whatIsEnemy);
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
                timeBtwAttack = startTimeBtwAttack;
            }
        } else
           {
               timeBtwAttack -= Time.deltaTime;
           }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Check to see if you have enough mana
            //Reduce the Mana by mana cost
            //Shoot Ranged Attack
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position,attackHitboxSize);
    }
}
