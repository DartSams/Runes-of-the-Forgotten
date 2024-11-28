using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class val : MonoBehaviour
{
    BoxCollider2D bc; // Reference to the player's BoxCollider2D component
    Rigidbody2D rb; // Reference to the player's Rigidbody2D component
    SpriteRenderer sprite; // Reference to the player's SpriteRenderer component
    DartagnanplayerController input = null; // Input handler for player controls
    Vector2 moveVector; // Stores the movement input vector
    float moveSpeed = 5f; // Speed at which the player moves
    bool isPaused = false; // Toggle for pausing the game
    public int score = 000000; // Initial player score
    float timeLimit = 300; // Time limit for the game
    public float jumpForce = 5f; // Force applied when player jumps
    private bool isGrounded = false; // Flag to check if player is on the ground
    Animator animator;
    void Awake()
    {
        // Initialize components and set initial values for score and coins UI
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        input = new DartagnanplayerController();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Call movement function and check if player is grounded
        Move();

        if (transform.position.y < 0.5f)
        {
            isGrounded = true;
        }
    }

    // Enable input events for player actions
    private void OnEnable()
    {
        input.Enable();
        input.player.movement.performed += OnMovePerformed;
        input.player.movement.canceled += OnMoveCancelled;
        input.player.jump.performed += OnJumpPerformed;
    }

    // Disable input events when not needed
    private void OnDisable()
    {
        input.Disable();
        input.player.movement.performed -= OnMovePerformed;
        input.player.movement.canceled -= OnMoveCancelled;
        input.player.jump.performed -= OnJumpPerformed;
    }

    // Handle horizontal movement input
    private void OnMovePerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
        if (moveVector.x > 0)
        {
            sprite.flipX = false; // Face right
        }
        else if (moveVector.x < 0)
        {
            sprite.flipX = true; // Face left
        }
        animator.SetFloat("yDir", moveVector.x);

    }

    // Stop movement when input is canceled
    private void OnMoveCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
        animator.SetFloat("yDir", moveVector.x);
    }

    // Handle player movement based on input
    private void Move()
    {
        Vector2 moveDirection = moveVector.normalized;
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }

    // Toggle pause and unpause functionality
    private void OnPausePerformed(InputAction.CallbackContext value)
    {
        isPaused = !isPaused;

        // Pause or unpause the game by manipulating Time.timeScale
        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Time.timeScale = 1f; // Unpause the game
        }
    }

    // Handle jump input, applying force if player is grounded
    private void OnJumpPerformed(InputAction.CallbackContext value)
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    // Reset grounded status when player leaves collision
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (transform.position.y > 0.5f)
        {
            isGrounded = false;
        }
    }
}