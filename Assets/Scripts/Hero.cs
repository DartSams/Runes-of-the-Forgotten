using System.Collections;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public CapsuleCollider2D capsuleCollider { get; private set; }
    public Movement movement { get; private set; }

    public PlayerSpriteRenderer PlayerRenderer;
    private PlayerSpriteRenderer activeRenderer;

    public bool player => PlayerRenderer.enabled;

    public int maxHealth = 100;

    public int currentHealth;

    private Health health;

    public Health healthbar;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        movement = GetComponent<Movement>();
        activeRenderer = PlayerRenderer;
    }

    private void Start()
    {
        health = GetComponent<Health>();
        currentHealth = maxHealth;
        //healthbar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthbar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            //Health.Die();
        }
    }
}
