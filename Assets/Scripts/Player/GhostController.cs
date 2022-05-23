using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GhostController : MonoBehaviour
{
    Rigidbody2D ghostRigidbody;
    SpriteRenderer spriteRenderer;
    float direction;

    // Start is called before the first frame update
    void Start()
    {
        ghostRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.flipX = direction < 0 || (spriteRenderer.flipX && direction == 0);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 velocity = context.ReadValue<Vector2>();
        Debug.Log(velocity);
        direction = velocity.x;

        velocity = velocity.normalized;

        ghostRigidbody.velocity = velocity * 7;
    }
}
