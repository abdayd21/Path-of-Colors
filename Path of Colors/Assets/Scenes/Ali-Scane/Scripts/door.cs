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
        anim.SetBool("Open", true);
        locked = false;
        doorCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (locked)
            {
                // Kap� kilitliyse ve oyuncu kap�y� a�mak i�in anahtar ta��m�yorsa, uyar� ver
                Debug.Log("Kap� kilitli! Anahtar� bul ve kap�y� a�.");
            }
            else
            {
                anim.SetBool("Open", false);
                locked = true;
            }
        }
    }
}
