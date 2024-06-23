using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Sprite emptyHeart; // Bo� kalp simgesi
    public Sprite fullHeart; // Dolu kalp simgesi
    public Image[] hearts; // Kalp simgeleri i�in Image dizisi

    public Health playerHealth; // Oyuncunun sa�l�k scripti referans�

    // Update is called once per frame
    void Update()
    {
        int health = (int)playerHealth.currentHealth; // Oyuncunun mevcut sa�l���
        int maxHealth = (int)playerHealth.maxHealth; // Oyuncunun maksimum sa�l���

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart; // Sa�l�k mevcutsa dolu kalp g�ster
            }
            else
            {
                hearts[i].sprite = emptyHeart; // Sa�l�k yoksa bo� kalp g�ster
            }

            if (i < maxHealth)
            {
                hearts[i].enabled = true; // Kalp simgesini etkinle�tir
            }
            else
            {
                hearts[i].enabled = false; // Kalp simgesini devre d��� b�rak
            }
        }
    }
}
