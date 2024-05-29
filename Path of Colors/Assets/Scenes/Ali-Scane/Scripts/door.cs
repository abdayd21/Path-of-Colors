using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public bool locked = true;
    private Animator anim;
    private Collider2D doorCollider;

    void Start()
    {
        anim = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();
    }

    public void UnlockDoor()
    {
        anim.SetTrigger("Open");
        locked = false;
        doorCollider.isTrigger = true;
    }

    public void LockDoor()
    {
        anim.SetTrigger("Close");
        locked = true;
        doorCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !locked)
        {
            UnlockDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !locked)
        {
            LockDoor();
        }
    }
}