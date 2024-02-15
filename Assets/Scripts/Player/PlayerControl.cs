using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControl : MonoBehaviour
{
    public FixedJoystick Joystick;
    Rigidbody2D rb;
    Vector2 move;
    [SerializeField] private float moveSpeed = 5f;
    private bool _faceRight = true;

    private Animator anim;
    public VectorValue pos;

    public int health;
    public int maxHealth;

    private bool dead;

    private float timer;

    private RestartGame rg;
    public GameObject reset;
    public GameObject win;

    private Boss Bosshealth;


    private bool isDead;

    private void Awake()
    {
        transform.position = pos.initialValue;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (PlayerPrefs.HasKey("Player Speed") == false)
        {
            PlayerPrefs.SetInt("Player Speed", 5);
        }

        if (PlayerPrefs.HasKey("Player Health") == false)
        {
            PlayerPrefs.SetInt("Player Health", 100);
        }

        if (PlayerPrefs.HasKey("Player MaxHealth") == false)
        {
            PlayerPrefs.SetInt("Player MaxHealth", 100);
        }

        StatsUpdate();
    }

    private void Update()
    {
        move.x = Joystick.Horizontal;
        move.y = Joystick.Vertical;

        anim.SetFloat("hoz", Mathf.Abs(move.x));

        if (move.x > 0 && _faceRight == false || move.x < 0 && _faceRight == true)
        {
            transform.Rotate(0f, -180f, 0f);
            _faceRight = !_faceRight;
        }

        if (health > maxHealth)
        {
            health = maxHealth;
            PlayerPrefs.SetInt("Player Health", health);
            StatsUpdate();
        }

        if (health <= 0 && !isDead)
        {
            isDead = true;
            reset.SetActive(true);
            pos.initialValue = pos.defaultValue;
        }

        Bosshealth = GetComponent<Boss>();

        if(Bosshealth.health <= 0 && !isDead)
        {
            isDead = true;
            win.SetActive(true);
            pos.initialValue = pos.defaultValue;
        }
    }

    public void StatsUpdate()
    {
        moveSpeed = PlayerPrefs.GetInt("Player Speed");
        health = PlayerPrefs.GetInt("Player Health");
        maxHealth = PlayerPrefs.GetInt("Player MaxHealth");

        ShopMenu.HealthPotion = PlayerPrefs.GetInt("HealthPotion");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(move.x * moveSpeed, move.y * moveSpeed);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        PlayerPrefs.SetInt("Player Health", health);
        StatsUpdate();

        if (health > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            if(!dead)
            {
                anim.SetTrigger("die");

                Joystick.enabled = false;

                if (GetComponent<PlayerCombat>() != null)
                    GetComponent<PlayerCombat>().enabled = false;

                if (GetComponent<CircleCollider2D>() != null)
                    GetComponent<CircleCollider2D>().enabled = false;

                dead = true;
            }
        }
    }

    public void healing()
    {
        ShopMenu.HealthPotion = PlayerPrefs.GetInt("HealthPotion");

        if(ShopMenu.HealthPotion > 0)
        {
            if(health < maxHealth)
            {
                PlayerPrefs.SetInt("HealthPotion", ShopMenu.HealthPotion - 1);
                PlayerPrefs.SetInt("Player Health", health + 25);
                StatsUpdate();
            }
        }
    }
}

