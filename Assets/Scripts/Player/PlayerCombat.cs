using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator anim;

    //Ближний бой
    public Transform attackPoint;
    public float attackRange = 0.5f;
    [SerializeField] public int damage;
    private float cooldownTime = Mathf.Infinity;
    [SerializeField] private float attackCooldown;

    public LayerMask enemyLayers;

    //Дальний бой
    [SerializeField] public static int ShootDamage = 20;

    public GameObject bullet;
    public Transform bulletPos;

    private Boss BossHealth;

    private float ShootcooldownTime = Mathf.Infinity;
    [SerializeField] private float magicooldown;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (PlayerPrefs.HasKey("Player damage") == false)
        {
            PlayerPrefs.SetInt("Player damage", 20);
        }

        if (PlayerPrefs.HasKey("Player ShootDamage") == false)
        {
            PlayerPrefs.SetInt("Player ShootDamage", 20);
        }

        StatsUpdate1();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            shoot();
        }
        cooldownTime += Time.deltaTime;

        ShootcooldownTime += Time.deltaTime;
    }

    public void Attack()
    {
        if (cooldownTime >= attackCooldown)
        {
            anim.SetTrigger("Attack");
            cooldownTime = 0;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Health>().TakeHit(damage);
            }

            Collider2D[] hitBoss = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D boss in hitBoss)
            {
                boss.GetComponent<Boss>().TakeHit(damage);
            }
        }
    }

    public void BossAttack()
    {
        if (cooldownTime >= attackCooldown)
        {
            anim.SetTrigger("Attack");
            cooldownTime = 0;

            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);

            if (hit != null)
            {
                BossHealth = hit.transform.GetComponent<Boss>();
                
            }

            BossHealth.TakeHit(damage);
        }
    }

    public void shoot()
    {
        if(ShootcooldownTime >= magicooldown)
        {
            ShootcooldownTime = 0;
            Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        }
        
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void StatsUpdate1()
    {
        damage = PlayerPrefs.GetInt("Player damage");
        ShootDamage = PlayerPrefs.GetInt("Player ShootDamage");
    }
}