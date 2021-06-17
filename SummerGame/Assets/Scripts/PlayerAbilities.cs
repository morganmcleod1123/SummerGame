using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    private GameManager gm;
    private Rigidbody2D rigidbody2D;
    public GameObject projectile;
    public Vector2 velocity;
    public Vector2 offset = new Vector2(0.4f, 0.1f);
    bool faceRight;

    public float fireBallManaCost;
    public float teleportManaCost;

    public GameObject teleportPos;
    private int dashCount;
    public float dashDistance = 15f;
    bool isDashing;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        isDashing = false;
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (GameManager.Instance.enoughMana(teleportManaCost))
            {
                blink();
                gm.decreaseMana(teleportManaCost);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isDashing && faceRight)
        {
            Debug.Log("Right Dash");
            StartCoroutine(Dash(1f));
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isDashing && !faceRight)
        {
            Debug.Log("Left Dash");
            StartCoroutine(Dash(-1f));
        }
    }
    IEnumerator Dash(float direction)
    {
        Debug.Log("Inside the Dash");
        isDashing = true;
        //dashCount--;
        //trail.emitting = true;
        //dust.Play();
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
        rigidbody2D.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity = rigidbody2D.gravityScale;
        rigidbody2D.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        //trail.emitting = false;
        isDashing = false;
        rigidbody2D.gravityScale = gravity;
    }

    void blink()
    {
        transform.position = teleportPos.transform.position;
    }
}
