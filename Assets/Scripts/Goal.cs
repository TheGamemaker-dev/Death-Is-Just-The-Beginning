using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    Animator anim;
    BoxCollider2D boxCollider;

    private void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        boxCollider.enabled = (KeyManager.keyTotal == 0);
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
