using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float speed;

    public GameObject bullet;
    public Transform bulletPos;

    private Transform player;
    public Transform homePos;

    private float timer;

    public float stoppingDistance;
    public float retreatDistance;

    public float maxRange;

    bool angry = false;
    bool goBack = false;

    private bool flip;

    public static int damage = 20;

    private Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        Vector3 scale = transform.localScale;

        if (distance > stoppingDistance && distance <= maxRange)
        {
            angry = true;
            goBack = false;

            timer += Time.deltaTime;

            if (timer > 1)
            {
                timer = 0;
                anim.SetTrigger("Shoot");
            }
        }
        else if (distance < stoppingDistance && distance > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (distance < retreatDistance)
        {
            anim.SetTrigger("walk");
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if (distance >= maxRange)
        {
            goBack = true;
            angry = false;
        }

        if (angry == true)
        {
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
        else if (goBack == true)
        {
            anim.SetTrigger("walk");

            if (homePos.transform.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            }
            else
            {
                scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            }

            transform.localScale = scale;
        }
    }

    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
