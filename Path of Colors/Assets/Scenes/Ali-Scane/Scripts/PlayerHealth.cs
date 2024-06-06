using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxhealth = 3;
    

    public SpriteRenderer playerSr;
    public CharControl playerMovement;

    void Start()
    {
        health = maxhealth;
        
    }

    public void TakeDamage(int amount )
    {
        health -= amount;
        if(health <= 0)
        {
            playerSr.enabled = false;
            playerMovement.enabled = false;
        }

        
    }

}
    


