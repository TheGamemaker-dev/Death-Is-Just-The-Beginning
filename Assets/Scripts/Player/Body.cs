using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class Body : MonoBehaviour
{
    Rigidbody2D bodyRigidbody;
    SpriteRenderer spriteRenderer;
    Player player;
    PlayerInput playerInput;
    Animator animator;

    float direction = 0;
    bool onGround;
    bool isJumping;

    private void Start()
    {
        bodyRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<Player>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
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
            if(!onGround){
                animator.SetBool("isJumping", false);
            }
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
        if (context.started)
        {
            animator.SetBool("isMoving", true);
        }
        else if (context.canceled)
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (onGround && context.started)
        {
            bodyRigidbody.velocity = new Vector2(bodyRigidbody.velocity.x, 16.5f);
            animator.SetBool("isJumping", true);
        }
    }
}

//beans
