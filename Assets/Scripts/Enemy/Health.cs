using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;

    private Animator anim;
    private bool dead;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        check();
    }

    void check()
    {
        ShopMenu.coins = PlayerPrefs.GetInt("coins");
    }

    public void TakeHit(int damage)
    {
        health -= damage;

        if(health > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            if (!dead)
            {
                if (GetComponent<EnemyP>() != null)
                    GetComponent<EnemyP>().enabled = false;

                if (GetComponent<EnemyShooting>() != null)
                    GetComponent<EnemyShooting>().enabled = false;

                if (GetComponent<BoxCollider2D>() != null)
                    GetComponent<BoxCollider2D>().enabled = false;

                anim.SetTrigger("die");

                PlayerPrefs.SetInt("coins", ShopMenu.coins + 5);
                check();
                
                if(health < 0)
                {
                    Destroy(gameObject, 5f);
                }

                dead = true;
            }
        }     
    }
}