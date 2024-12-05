using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerControllerManual : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of movement
    public float jumpForce = 5f;  // Force applied when jumping
    private Rigidbody2D rb;       // Reference to the Rigidbody component
    private bool isGrounded = true; // To check if the player is grounded
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    public GameObject attackPoint;
    public LayerMask enemyLayer;
    public float radius;

    public int health;

    public ManageGameOver GameOver;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();// Get the Rigidbody component on the object
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MovePlayer();      // Call the method to move the player
        Jump();            // Call the method to handle jumping
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("RunesLevel");
        }
    }

    int getHealth()
    {

        return health;
    }
    public void takeDamage(int damage)
    {

        if (damage >= health)
        {
            //anim.SetTrigger("death");
            gameObject.SetActive(false);

            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            GameOver.gameOver();

        }
        else
        {
            health-= damage;

        }
    }

    public void Attack()
    {

        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemyLayer);

        foreach(Collider2D enemy2 in enemy)
        {

            Debug.Log("Hit enemy");
            sk_enemy_ai eh = enemy2.GetComponent<sk_enemy_ai>();

            eh.takeDamage(5);
        }


    }

    

    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPoint.transform.position, radius);
    }

    void MovePlayer()
    {
        // Get input from the player (WASD or arrow keys)
        float moveX = Input.GetAxis("Horizontal");  // A/D or Left/Right arrow
        float moveZ = Input.GetAxis("Vertical");    // W/S or Up/Down arrow

        float fire = Input.GetAxis("Fire1");

        if (moveX < 0)
        {
            // Moving left, flip the sprite
            spriteRenderer.flipX = true;
        }
        else if (moveX > 0)
        {
            // Moving right, unflip the sprite
            spriteRenderer.flipX = false;
        }


        Vector2 check = new Vector2(moveX, moveZ);


        if (fire != 0)
        {

            anim.SetBool("isAttacking", true);


        }

        else
        {
            anim.SetBool("isAttacking", false);
        }

        if (check != Vector2.zero)
        {
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
            
        }



        // Apply movement to the Rigidbody (without jumping)
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Move the Rigidbody (using velocity for smooth movement)
        rb.velocity = new Vector3(move.x * moveSpeed, rb.velocity.y, move.z * moveSpeed);
    }

    void Jump()
    {
        // Check if the player presses the Jump button (Space) and if grounded
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Add upward force to the Rigidbody for jumping
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;  // Set grounded to false until landing
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("damage"))
        {

            takeDamage(1);

            


        }

        
    }

}

    
       



