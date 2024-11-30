using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartagnanenemyScript : MonoBehaviour
{
    public Animator anim;
    private GameObject target;
    private PolygonCollider2D pc;
    public CircleCollider2D detectRadius;
    SpriteRenderer sr;
    public int maxHealth = 100;
    private int currentHealth;
    private float attackRange = 2.5f; 
    private float chaseSpeed = 2.0f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        pc = GetComponent<PolygonCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        triggerIdle();
        currentHealth = maxHealth;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
            if (distanceToTarget < attackRange)
            {
                anim.SetBool("run", false);
                anim.SetBool("idle", false);
                attack();
            }
            else
            {
                chase();
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, chaseSpeed * Time.deltaTime);
            }

            if (target.transform.position.x > transform.position.x)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            target = collision.gameObject;
        }
    }


    private void attack()
    {
        
        anim.SetTrigger("attack");
    }

    void hitEnemy()
    {
        target.gameObject.GetComponent<MainPlayerScript>().TakeDamage(25);
    }

    private void chase()
    {
        anim.SetBool("run", true);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        anim.SetTrigger("take hit");

        if (currentHealth <= 0)
        {
            target = null;
            //pc.enabled = false;
            Death();
            Destroy(gameObject);
            persistentManager.instance.UpdateEnemyCount();
        }
    }

    private void Death()
    {
        anim.SetTrigger("death");
        spawnLoot();
    }

    public void stopAttack()
    {
        anim.SetBool("attack", false);
    }

    public void triggerIdle()
    {
        anim.SetBool("idle", true);
    }

    void spawnLoot()
    {
        //spawn loot when killed
    }
}