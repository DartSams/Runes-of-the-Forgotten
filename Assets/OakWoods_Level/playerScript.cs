using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class playerScript : MonoBehaviour
{
    BoxCollider2D bc;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    PlayerController input = null;
    Animator anim;
    Vector2 moveVector;
    public int moveSpeed = 5;
    bool isPaused = false;
    public int score = 000000;
    public int coins;
    float timeLimit = 300;

    public float jumpForce = 10f;
    private bool isGrounded = false;

    public GameObject target;
    public int power = 5;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //gets the rigidbody component of the current object
        sprite = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        input = new PlayerController();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();

        if (transform.position.y < 0.5f)
        {
            isGrounded = true;
        }

    }



    private void OnEnable()
    {
        input.Enable();
        input.Player.movement.performed += OnMovePerformed;
        input.Player.movement.canceled += OnMoveCancelled;
        input.Player.jump.performed += OnJumpPerformed;
        input.Player.attack.performed += OnAttackPerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.movement.performed -= OnMovePerformed;
        input.Player.movement.canceled -= OnMoveCancelled;
        input.Player.jump.performed -= OnJumpPerformed;
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
        anim.SetFloat("yDirection", moveVector.x);
    }

    private void OnMoveCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
        anim.SetFloat("yDirection", moveVector.x);
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
            anim.SetFloat("xDirection", 1.29f);
            StartCoroutine(startFall());
            isGrounded = false;
        }
    }

    IEnumerator startFall()
    {
        yield return new WaitForSeconds(0.75f);
        anim.SetFloat("xDirection", -1.29f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "ground")
        {
            anim.SetFloat("xDirection", 0);
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

    

}