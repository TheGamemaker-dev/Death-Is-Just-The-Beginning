using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;

public class Body : MonoBehaviour
{
    Rigidbody2D bodyRigidbody;
    SpriteRenderer spriteRenderer;
    Player player;
    Animator animator;
    GameObject flashlight;

    float direction = 0;
    bool onGround;
    float flashlightRotation = -90;

    private void Start()
    {
        bodyRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<Player>();
        animator = GetComponent<Animator>();
        flashlight = GetComponentInChildren<Light2D>().gameObject;
    }
    private void Update()
    {
        if (!player.inGhostMode)
        {
			bodyRigidbody.velocity = new Vector2(7 * direction, bodyRigidbody.velocity.y);
        }
        spriteRenderer.flipX = direction < 0 || (spriteRenderer.flipX && direction == 0);
        UpdateFlashlight();
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

    public void FlashlightChange(InputAction.CallbackContext context)
	{
        float input = context.ReadValue<float>();
        float angle = input * 35;
        flashlightRotation = angle-90;
	}

    void UpdateFlashlight()
	{
        if ((spriteRenderer.flipX && flashlightRotation < 0) || (!spriteRenderer.flipX && flashlightRotation > 0))
        {
            flashlightRotation = -flashlightRotation;
        }

        flashlight.transform.eulerAngles = new Vector3(0, 0, flashlightRotation);
    }
}

//beans
