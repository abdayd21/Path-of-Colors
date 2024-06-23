using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Sprite emptyHeart; // Boþ kalp simgesi
    public Sprite fullHeart; // Dolu kalp simgesi
    public Image[] hearts; // Kalp simgeleri için Image dizisi

    public Health playerHealth; // Oyuncunun saðlýk scripti referansý

    // Update is called once per frame
    void Update()
    {
        int health = (int)playerHealth.currentHealth; // Oyuncunun mevcut saðlýðý
        int maxHealth = (int)playerHealth.maxHealth; // Oyuncunun maksimum saðlýðý

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart; // Saðlýk mevcutsa dolu kalp göster
            }
            else
            {
                hearts[i].sprite = emptyHeart; // Saðlýk yoksa boþ kalp göster
            }

            if (i < maxHealth)
            {
                hearts[i].enabled = true; // Kalp simgesini etkinleþtir
            }
            else
            {
                hearts[i].enabled = false; // Kalp simgesini devre dýþý býrak
            }
        }
    }
}
