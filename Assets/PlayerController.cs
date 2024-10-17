using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of movement
    public float jumpForce = 5f;  // Force applied when jumping
    private Rigidbody2D rb;       // Reference to the Rigidbody component
    private bool isGrounded = true; // To check if the player is grounded

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody component on the object
    }

    void Update()
    {
        MovePlayer();      // Call the method to move the player
        Jump();            // Call the method to handle jumping
    }

    void MovePlayer()
    {
        // Get input from the player (WASD or arrow keys)
        float moveX = Input.GetAxis("Horizontal");  // A/D or Left/Right arrow
        float moveZ = Input.GetAxis("Vertical");    // W/S or Up/Down arrow

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
            //isGrounded = false;  // Set grounded to false until landing
        }
    }

    // Check if the player collides with the ground
    private void OnCollisionEnter(Collision collision)
    {
        // If the player touches a surface tagged as "Ground," reset grounded status
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}


