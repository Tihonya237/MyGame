using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform player;

    private Rigidbody2D rb;

    public float speed;

    private PlayerControl playerHealth;
    private Health enemyHealth;

    private Boss BossHealth;

    public bool Pl = false;
    public bool boss = false;
    private Animator anim;

    private Vector2 target;

    private float timer;

    private PlayerCombat pc;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        if (Pl == true)
        {
            rb.velocity = transform.right * speed;
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            target = new Vector2(player.position.x, player.position.y);

            Vector3 dir = player.transform.position - transform.position;
            rb.velocity = new Vector2(dir.x, dir.y).normalized * speed;

            float rot = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        }

        if(boss == true)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            target = new Vector2(player.position.x, player.position.y);

            Vector3 dir = player.transform.position - transform.position;
            rb.velocity = new Vector2(dir.x, dir.y).normalized * speed;

            float rot = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 180);
        }
    }

    private void Update()
    {

        timer += Time.deltaTime;

        if(timer > 3)
        {
            timer = 0;
            Destroy(gameObject);
       }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(Pl == true)
        {
            if (other.tag == "Enemy")
            {
                enemyHealth = other.GetComponent<Health>();
                BossHealth = other.GetComponent<Boss>();

                if(enemyHealth != null)
                {
                    enemyHealth.TakeHit(PlayerCombat.ShootDamage);
                    
                }

                if(BossHealth != null)
                {
                    BossHealth.TakeHit(PlayerCombat.ShootDamage);
                }
                Destroy(gameObject);
            }  
        }
        else
        {
            if (other.tag == "Player")
            {
                playerHealth = other.GetComponent<PlayerControl>();
                playerHealth.TakeDamage(EnemyShooting.damage);
              
                Destroy(gameObject);
            }
        } 
    }
}
