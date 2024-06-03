using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public bool isPressed = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("soul") || other.CompareTag("Player"))
        {
            isPressed = true;
            anim.SetBool("OpenBut", true);

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("soul") || other.CompareTag("Player"))
        {
            isPressed = false;
            anim.SetBool("OpenBut", false);
        }
    }
}

