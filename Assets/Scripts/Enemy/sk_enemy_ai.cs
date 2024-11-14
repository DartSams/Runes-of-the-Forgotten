using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class sk_enemy_ai : MonoBehaviour
{
   
     public float attackCooldown;
     public float range;
     public int damage;
     public int health;
     public float colliderDistance;
     public BoxCollider2D boxCollider;
     public LayerMask playerLayer;
     private float cooldownTimer = Mathf.Infinity;

    private EnemyPatrol enemyPatrol;

    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        
    }

    public void takeDamage(int damage)
    {

        if (damage >= health)
        {
            Destroy(gameObject);
        }

        else
        {

           // health -= damage;
           gameObject.SetActive(false);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {

            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
            }

            
               

        }
    }

    

    private bool PlayerInSight()
    {
        


        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);


        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
