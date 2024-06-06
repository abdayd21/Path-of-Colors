using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsansorButton : MonoBehaviour
{
    public AsansorScript elevator;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            elevator.StartMoving();
        }
    }
}