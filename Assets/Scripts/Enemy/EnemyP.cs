using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyP : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxRange;
    [SerializeField]
    private float minRange;

    private Animator anim;
    private Transform target;
    public Transform homePos;

    bool angry = false;
    bool goBack = false;
    bool Attack = false;

    private bool flip;

    [SerializeField] private float attackColldown;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask playerLayer;

    private float cooldownTime = Mathf.Infinity;

    private PlayerControl playerHealth;

    public Transform attackPoint;
    public float attackRange = 0.5f;


    private void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 scale = transform.localScale;

        if (Vector2.Distance(target.position, transform.position) <= maxRange)
        {
            angry = true;
            goBack = false;
            Attack = false;

            if (Vector2.Distance(transform.position, target.position) <= minRange)
            {
                angry = false;
                Attack = true;

                if (PlayerInSight())
                {
                    cooldownTime += Time.deltaTime;

                    if (cooldownTime >= attackColldown)
                    {
                        cooldownTime = 0;
                        anim.SetTrigger("MeleeAttack");
                    }
                }
            }
        }

        if(Vector2.Distance(transform.position, target.position) >= maxRange)
        {
            Attack = false;
            goBack = true;
            angry = false;
        }

        if (angry == true)
        {
            FollowPlayer();

            if (target.transform.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            }
            else
            {
                scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            }
            transform.localScale = scale;
        }
        else if (goBack == true)
        {
            GoHome();

            if (homePos.transform.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            }
            else
            {
                scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            }

            transform.localScale = scale;
        } else if (Attack == true)
        {
            PlayerInSight();
        }
    }

    public void FollowPlayer()
    {
        anim.SetBool("WALK", true);
        anim.SetTrigger("walk");
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void GoHome()
    {
        transform.position = Vector2.MoveTowards(transform.position, homePos.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, homePos.position) == 0)
        {
            anim.SetBool("WALK", false);
        }
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
}
