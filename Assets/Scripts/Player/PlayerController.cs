using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    SpriteRenderer spriteRenderer;
    float direction = 0;
    bool onGround;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        playerRigidbody.velocity = new Vector2(7 * direction, playerRigidbody.velocity.y);
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
        if (onGround && context.phase == InputActionPhase.Started)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 16.5f);
        }
    }

}
