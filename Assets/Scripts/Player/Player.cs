using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour
{
    public readonly float restartTime = 2f;

    public bool inGhostMode = false;
    public float restartHoldTime = 0;
    public bool holdingRestart = false;

    public GameObject body,
        ghost;

    void Start()
    {
        DisableGhost();
        EnableBody();
    }

    private void Update()
    {
        restartHoldTime = holdingRestart ? restartHoldTime + Time.deltaTime : 0;

        if (restartHoldTime >= restartTime)
        {
            GameManager.singleton.LoadLevel(GameManager.singleton.currentLevel);
        }
    }

    public void SwitchMode(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started)
        {
            return;
        }

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

    public void Restart(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            holdingRestart = true;
        }
        else if (context.canceled)
        {
            holdingRestart = false;
        }
    }

    public void OnFinishLevel()
    {
        body.SetActive(false);
        ghost.SetActive(false);
    }

    void DisableBody()
    {
        body.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        body.GetComponent<PlayerInput>().DeactivateInput();
        body.GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f, 1f);
        body.GetComponentInChildren<Light2D>().intensity = 0;
    }

    void DisableGhost()
    {
        ghost.GetComponent<PlayerInput>().DeactivateInput();
        ghost.SetActive(false);
    }

    void EnableBody()
    {
        body.GetComponent<PlayerInput>().ActivateInput();
        body.GetComponent<PlayerInput>()
            .SwitchCurrentControlScheme(new InputDevice[] { Keyboard.current, Mouse.current });
        body.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

        body.GetComponentInChildren<Light2D>().intensity = 1;
    }

    void EnableGhost()
    {
        ghost.SetActive(true);
        ghost.transform.position = body.transform.position;
        ghost.GetComponent<PlayerInput>().ActivateInput();
        ghost
            .GetComponent<PlayerInput>()
            .SwitchCurrentControlScheme(new InputDevice[] { Keyboard.current, Mouse.current });
    }
}
