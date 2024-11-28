using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Animation : MonoBehaviour
{
    public Sprite[] sprites; // Array to hold the different sprites for the animation
    public float framerate = 1f / 6f; // Framerate for the animation 

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private int frame; // Tracks the current frame of the animation

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        // Start the animation when the object is enabled (repeatedly call Animate method using intervals)
        InvokeRepeating(nameof(Animate), framerate, framerate);
    }

    private void OnDisable()
    {
        // Stop the animation when the object is disabled
        CancelInvoke();
    }

    private void Animate()
    {
        // Increment the frame index to move to the next sprite
        frame++;

        // If the frame exceeds the number of sprites, loop back to the first frame
        if (frame >= sprites.Length)
        {
            frame = 0;
        }

        // Check if the frame is within valid range and update the sprite to the current frame
        if (frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
        }
    }
}
