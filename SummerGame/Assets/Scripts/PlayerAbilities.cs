using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public GameObject projectile;
    public Vector2 velocity;
    public Vector2 offset = new Vector2(0.4f, 0.1f);
    bool faceRight;

    public float fireBallManaCost;
    private GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
    }
    void Update()
    {
        if(transform.localScale.x > 0)
        {
            faceRight = true;
        } else
        {
            faceRight = false;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GameManager.Instance.enoughMana(fireBallManaCost))
            {
                GameObject fireball = (GameObject) Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);
                fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);
                if (!faceRight)
                {
                        Vector3 Scaler = fireball.transform.localScale;
                        Scaler.x *= -1;
                        fireball.transform.localScale = Scaler;
                }
                
                gm.decreaseMana(fireBallManaCost);
            }
        }
        Debug.Log(faceRight);
    }
}
