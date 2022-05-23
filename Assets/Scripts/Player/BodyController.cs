using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BodyController : MonoBehaviour
{
    Rigidbody2D bodyRigidbody;
    SpriteRenderer spriteRenderer;
    Player player;
    PlayerInput playerInput;
    float direction = 0;
    bool onGround;

    private void Start()
    {
        bodyRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<Player>();
        playerInput = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        if (!player.inGhostMode)
        {
            bodyRigidbody.velocity = new Vector2(7 * direction, bodyRigidbody.velocity.y);
        }
        spriteRenderer.flipX = direction < 0 || (spriteRenderer.flipX && direction == 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (onGround && context.phase == InputActionPhase.Started && !player.inGhostMode)
        {
            bodyRigidbody.velocity = new Vector2(bodyRigidbody.velocity.x, 16.5f);
        }
    }

}
