using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Collider2D capCollider;

    private Vector2 velocity;            // Current velocity of the player character
    private float inputAxis;             // Horizontal input from the player (left/right movement)

    public float movementSpeed = 8f;     // Speed at which the player moves horizontally
    public float maxJumpHeight = 6f;     // The maximum height of the player's jump
    public float maxJumpTime = 1f;       // The time it takes to reach the maximum jump height

    // Calculate jump strength based on max jump height and time
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);

    // Calculate gravity force based on the max jump height and time
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2f);

    // Check if the player is currently running (moving horizontally)
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;

    // Check if the player is falling (moving downwards)
    public bool falling => velocity.y < 0f && !grounded;

    public bool grounded { get; private set; }  // Whether the player is grounded or not
    public bool jumping { get; private set; }   // Whether the player is in the air and jumping

    public LayerMask GroundLayer;  // Layer for ground detection
    public LayerMask PipeLayer;    // Layer for pipe detection

    private void Awake()
    {
        // Getting the Rigidbody2D and Collider2D components on the GameObject
        rigidbody = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        HorizontalMovement();   // Handle horizontal movement based on user input
        CheckGrounded();        // Check if the player is grounded

        // If grounded, allow grounded movement (jumping)
        if (grounded)
        {
            GroundedMovement();
        }

        ApplyGravity();  // Apply gravity to the player to bring them back down if they are falling
    }

    private void FixedUpdate()
    {
        // Update position of the Rigidbody2D by applying the calculated velocity
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;
        rigidbody.MovePosition(position);
    }

    private void HorizontalMovement()
    {
        // Get horizontal input from the player (left or right)
        inputAxis = Input.GetAxis("Horizontal");

        // Smoothly change velocity based on input and movement speed
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * movementSpeed, movementSpeed * Time.deltaTime);

        // Adjust this line to handle pipe collision separately (checking for pipe collision)
        if (rigidbody.Raycast(Vector2.right * velocity.x)) // Check collision 
        {
            velocity.x = 0f;  // Stop horizontal movement entirely if colliding with a pipe
        }

        //Flip the player's sprite based on movement direction (left or right)
        if (velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;  // Facing right (no rotation)
        }
        else if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);  // Facing left 
        }
    }

    private void CheckGrounded()
    {
        // Extra height to check for the ground below
        float extraHeight = 0.1f;
        // The raycast will start at the player's position just below the collider
        Vector2 origin = new Vector2(capCollider.bounds.center.x, capCollider.bounds.min.y);

        // Check if the player is grounded by casting a ray downward and checking if it hits the ground
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, extraHeight, GroundLayer);

        grounded = hit.collider != null;  // Set grounded to true if the ray hits the ground

        // Draw a debug ray in the Scene view to visualize the grounded check (green for grounded, red for not)
        Debug.DrawRay(origin, Vector2.down * extraHeight, grounded ? Color.green : Color.red);
    }

    private void GroundedMovement()
    {
        // Prevent negative vertical velocity (the player can't fall through the ground)
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        // If the player presses the jump button, apply jump strength
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;  // Set the upward velocity for the jump
            jumping = true;  // Set jumping to true
        }
    }

    private void ApplyGravity()
    {
        // Check if the player is falling 
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");

        // Apply a stronger gravity when falling
        float multiplier = falling ? 2f : 1f;

        // Apply gravity to the vertical velocity.
        velocity.y += gravity * multiplier * Time.deltaTime;

        // Limit downward velocity to avoid excessive falling speed
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for collision with an enemy (using enemy layer)
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // If the player collides with an enemy from above (head), bounce off
            if (transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = jumpForce / 2f;  // Bounce off the enemy 
                jumping = true;  // Set jumping to true 
            }
        }
        // Check for collision with a pipe (using pipe layer)
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            // Stop horizontal movement if colliding with a pipe
            velocity.x = 0f;
        }
    }
}
