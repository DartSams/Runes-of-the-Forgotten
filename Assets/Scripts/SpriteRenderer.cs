using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSpriteRenderer : MonoBehaviour
{
    private Movement movement;
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite idle;
    public Sprite jump;
    public Animation run;

    private void Awake()
    {
        movement = GetComponentInParent<Movement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        // Check if 'run' has been assigned before trying to enable it
        if (run != null)
        {
            run.enabled = movement.running;
        }

        if (movement.jumping)
        {
            spriteRenderer.sprite = jump;
        }
        else if (!movement.running)
        {
            spriteRenderer.sprite = idle;
        }
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;

        // Check if 'run' has been assigned before disabling it
        if (run != null)
        {
            run.enabled = false;
        }
    }
}
