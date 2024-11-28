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

    public GameObject attackPoint;
    public float radius;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        
    }

    public void takeDamage(int damage)
    {

        if (damage >= health)
        {
            //Destroy(gameObject);
            gameObject.gameObject.SetActive(false);
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

    private void enemyAttack()
    {


        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

       
        RaycastHit2D hit = Physics2D.Raycast(attackPoint.transform.position, direction, 5.0f, playerLayer);
        if (hit.collider != null)
        {
            Debug.Log("Player hit: " + hit.collider.name);
            
            PlayerControllerManual pl = hit.collider.GetComponent<PlayerControllerManual>();

            if (pl != null)
            {

                pl.takeDamage(1);
            }
            else
            {
                Debug.Log("Player is null");
            }
        }



        /*
        Collider2D[] player = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius,playerLayer);


        Debug.Log(player);

        foreach (Collider2D player2 in player)
        {

            Debug.Log("Hit Player");
            PlayerControllerManual eh = player2.GetComponent<PlayerControllerManual>();

            
        }*/

    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPoint.transform.position, radius);
    }

    private bool PlayerInSight()
    {
        


        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);


        return hit.collider != null;
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }*/
}
