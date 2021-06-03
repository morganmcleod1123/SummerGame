using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePosition;

    public float fireBallManaCost;
    private GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GameManager.Instance.enoughMana(fireBallManaCost))
            {
                Instantiate(projectile, firePosition.position, firePosition.rotation);
                gm.decreaseMana(fireBallManaCost);
            }
        }
    }
}
