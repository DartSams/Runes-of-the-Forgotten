using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class aaron_move : MonoBehaviour
{
    DartagnanplayerController input = null;
    Vector2 moveVector;
    public int moveSpeed = 5;
    SpriteRenderer sprite;
    public float jumpForce = 10f;
    private bool isGrounded = false;
    BoxCollider2D bc;
    Rigidbody2D rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //gets the rigidbody component of the current object
        sprite = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        input = new DartagnanplayerController();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
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

    void OnAttackPerformed(InputAction.CallbackContext value)
    {
        anim.SetTrigger("attack");
    }
}
