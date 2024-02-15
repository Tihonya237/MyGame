using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health;

    public int damage;
    public int shootdamage;

    public float speed;

    public Transform attackPoint;
    public float attackRange = 0.5f;

    [SerializeField] private float attackColldown;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackShootColldown;

    private float cooldownTime = Mathf.Infinity;
    private float ShootcooldownTime = Mathf.Infinity;

    private PlayerControl playerHealth;

    private Animator anim;
    private Transform player;

    bool angry = false;
    bool Attack = false;
    bool shoot = false;

    private bool flip;

    private float timer;

    public GameObject bullet;
    public Transform bulletPos;

    private bool dead;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 scale = transform.localScale;

        if (Vector2.Distance(transform.position, player.position) <= 5)
        {
            angry = true;
            shoot = true;
            Attack = false;

            timer += Time.deltaTime;

            if (Vector2.Distance(transform.position, player.position) <= 1)
            {
                angry = false;
                Attack = true;
                shoot = false;

                if (PlayerInSight())
                {
                    cooldownTime += Time.deltaTime;

                    if (cooldownTime >= attackColldown)
                    {
                        cooldownTime = 0;
                        anim.SetTrigger("Attack");
                    }
                }
            }
        }

        if (angry == true)
        {
            FollowPlayer();

            if (player.transform.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            }
            else
            {
                scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            }
            transform.localScale = scale;
        }
        
        if (shoot == true)
        {
            if (timer > 5)
            {
                timer = 0;
                BossShoot();
            }
        }
    }

    void BossShoot()
    {
        Instantiate(Instantiate(bullet, bulletPos.position, Quaternion.identity));
    }
    public bool PlayerInSight()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (hit != null)
        {
            playerHealth = hit.transform.GetComponent<PlayerControl>();
        }

        return hit != null;
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }

    public void FollowPlayer()
    {
        anim.SetTrigger("Walk");
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    public void TakeHit(int damage)
    {
        health -= damage;

        if (health > 0)
        {
            anim.SetTrigger("hit");
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                if (GetComponent<Boss>() != null)
                    GetComponent<Boss>().enabled = false;

                if (GetComponent<BoxCollider2D>() != null)
                    GetComponent<BoxCollider2D>().enabled = false;

                if (health < 0)
                {
                    Destroy(gameObject, 5f);
                }

                dead = true;
            }

        }
    }

}
