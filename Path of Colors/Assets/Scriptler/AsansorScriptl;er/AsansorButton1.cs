using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsansorButton1 : MonoBehaviour
{
    public AsansorScript elevator;
    private bool playerInRange = false;

    void Update()
    {
        // Eğer oyuncu düğme alanındaysa ve 'E' tuşuna basarsa
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            elevator.StartMoving();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}