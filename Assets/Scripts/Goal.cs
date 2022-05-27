using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
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
