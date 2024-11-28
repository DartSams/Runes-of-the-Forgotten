using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class dartagnanplayerScript : MonoBehaviour
{
    BoxCollider2D bc;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    DartagnanplayerController input = null;
    Animator anim;
    Vector2 moveVector;
    public int moveSpeed = 5;
    bool isPaused = false;
    public int score = 000000;
    public int coins;
    float timeLimit = 300;
    public int maxHealth = 100;
    public int currentHealth;

    public float jumpForce = 10f;
    private bool isGrounded = false;
    public GameObject attackRange;
    public GameObject target;
    public int power = 5;
    int enemyCount;
    GameObject[] enemies;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //gets the rigidbody component of the current object
        sprite = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        input = new DartagnanplayerController();
        anim = GetComponent<Animator>();
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        enemyCount = enemies.Length;
        currentHealth = maxHealth;
    }
        
    void Update()
    {
        Move();

        if (transform.position.y < 0.5f)
        {
            isGrounded = true;
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Player has died");
        }

    }



    private void OnEnable()
    {
        input.Enable();
        input.player.movement.performed += OnMovePerformed;
        input.player.movement.canceled += OnMoveCancelled;
        input.player.jump.performed += OnJumpPerformed;
        input.player.attack.performed += OnAttackPerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.player.movement.performed -= OnMovePerformed;
        input.player.movement.canceled -= OnMoveCancelled;
        input.player.jump.performed -= OnJumpPerformed;
    }

    private void OnMovePerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
        if (moveVector.x > 0)
        {
            sprite.flipX = false;
        }
        else if (moveVector.x < 0)
        {
            sprite.flipX = true;
        }
<<<<<<< HEAD
        anim.SetFloat("yDirection", moveVector.x);
=======
        anim.SetFloat("yDir", moveVector.x);
>>>>>>> 948f37c74c36da36539a1c91fbfcc3708266851f
    }

    private void OnMoveCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
<<<<<<< HEAD
        anim.SetFloat("yDirection", moveVector.x);
=======
        anim.SetFloat("yDir", moveVector.x);
>>>>>>> 948f37c74c36da36539a1c91fbfcc3708266851f
    }

    private void Move()
    {
        Vector2 moveDirection = moveVector.normalized;
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }

    private void OnPausePerformed(InputAction.CallbackContext value)
    {
        //pauseMenu.active = true;
        isPaused = !isPaused;

        // Pause or unpause the game by manipulating Time.timeScale
        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
            //pausePanel.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // Unpause the game
            //pausePanel.gameObject.SetActive(false);
        }

    }

    private void OnJumpPerformed(InputAction.CallbackContext value)
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
<<<<<<< HEAD
            anim.SetFloat("xDirection", 1.29f);
=======
            anim.SetFloat("xDir", 1.29f);
>>>>>>> 948f37c74c36da36539a1c91fbfcc3708266851f
            StartCoroutine(startFall());
            isGrounded = false;
        }
    }

    IEnumerator startFall()
    {
        yield return new WaitForSeconds(0.75f);
<<<<<<< HEAD
        anim.SetFloat("xDirection", -1.29f);
=======
        anim.SetFloat("xDir", -1.29f);
>>>>>>> 948f37c74c36da36539a1c91fbfcc3708266851f
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "ground")
        {
<<<<<<< HEAD
            anim.SetFloat("xDirection", 0);
=======
            anim.SetFloat("xDir", 0);
>>>>>>> 948f37c74c36da36539a1c91fbfcc3708266851f
        }

        if (collision.transform.tag == "rune")
        { 
            persistentManager.instance.AddCoins(1);
            Destroy(collision.gameObject);
            //SceneManager.LoadScene("scene 2");

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (transform.position.y > 0.5f)
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.gameObject;
    }

    void OnAttackPerformed(InputAction.CallbackContext value)
    {
        anim.SetTrigger("attack");
        target.GetComponent<enemyScript>().TakeDamage(power);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            target = null;
            //pc.enabled = false;
        }
    }
}
