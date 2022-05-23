using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public bool inGhostMode = false;
    [SerializeField] GameObject body, ghost;

    void Start()
    {
        DisableGhost();
    }

    public void SwitchMode(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) { return; }

        inGhostMode = !inGhostMode;

        if (inGhostMode)
        {
            EnableGhost();
            DisableBody();
        }
        else
        {
            DisableGhost();
            EnableBody();
        }
    }

    void DisableBody()
    {
        body.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        body.GetComponent<PlayerInput>().DeactivateInput();
        body.GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f, 1f);
    }

    void DisableGhost()
    {
        ghost.GetComponent<PlayerInput>().DeactivateInput();
        ghost.SetActive(false);
    }

    void EnableBody()
    {
        body.GetComponent<PlayerInput>().ActivateInput();
        body.GetComponent<PlayerInput>().SwitchCurrentControlScheme(new InputDevice[] { Keyboard.current, Mouse.current });
        body.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    void EnableGhost()
    {
        ghost.SetActive(true);
        ghost.GetComponent<PlayerInput>().ActivateInput();
        ghost.GetComponent<PlayerInput>().SwitchCurrentControlScheme(new InputDevice[] { Keyboard.current, Mouse.current });
        ghost.transform.position = body.transform.position;
    }
}
