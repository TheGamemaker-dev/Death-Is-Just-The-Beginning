using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GhostController : MonoBehaviour
{
    Rigidbody2D ghostRigidbody;
    SpriteRenderer spriteRenderer;
    Vector2 velocity;
    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        ghostRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = velocity.x;
        velocity = velocity.normalized;

        ghostRigidbody.velocity = velocity * 7;

        spriteRenderer.flipX = direction < 0 || (spriteRenderer.flipX && direction == 0);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            velocity = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            velocity = Vector2.zero;
        }
    }
}
