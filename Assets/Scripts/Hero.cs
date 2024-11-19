using System.Collections;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public CapsuleCollider2D capsuleCollider { get; private set; }
    public Movement movement { get; private set; }

    public PlayerSpriteRenderer PlayerRenderer;
    private PlayerSpriteRenderer activeRenderer;

    public bool player => PlayerRenderer.enabled;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        movement = GetComponent<Movement>();
        activeRenderer = PlayerRenderer;
    }

    // public void Hit()
    // {
    //     Death();
    // }
}
