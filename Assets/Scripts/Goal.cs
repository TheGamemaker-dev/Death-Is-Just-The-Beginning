using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    Animator anim;
    BoxCollider2D boxCollider;
    bool isOpen;

    private void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    private void Update()
    {
        if (!isOpen)
        {
            isOpen = KeyManager.keyTotal == 0;
            if (isOpen)
            {
                boxCollider.enabled = true;
                anim.SetTrigger("onOpen");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("onClose");
            other.gameObject.GetComponentInParent<Player>().OnFinishLevel();
        }
    }

    public void NextLevel()
    {
        GameManager.singleton.NextLevel();
    }
}
