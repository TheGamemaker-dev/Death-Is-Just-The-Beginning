using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.Rendering.Universal;

public class Ghost : MonoBehaviour
{
    Rigidbody2D ghostRigidbody;
    SpriteRenderer spriteRenderer;
    Vector2 velocity;
    Light2D glow;
    GameObject body;

    // Start is called before the first frame update
    void Start()
    {
        ghostRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        glow = GetComponentInChildren<Light2D>();
        body = FindObjectOfType<Body>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        float direction = velocity.x;
        velocity = velocity.normalized;
        ghostRigidbody.velocity = velocity * 7;

        //sprite flips based on direction
        spriteRenderer.flipX = direction < 0 || (spriteRenderer.flipX && direction == 0);

        //glow less further out from body
        float sqrDistance = (body.transform.position - transform.position).sqrMagnitude;
        float dimming = (25 - sqrDistance) / 50;
        glow.intensity = Mathf.Max(dimming, 0);
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
