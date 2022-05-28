using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    bool triggered = false;

    private void Start()
    {
        KeyManager.keyTotal++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !triggered)
        {
            Destroy(this.gameObject);
            KeyManager.keyTotal--;
            triggered = true;
        }
    }
}
