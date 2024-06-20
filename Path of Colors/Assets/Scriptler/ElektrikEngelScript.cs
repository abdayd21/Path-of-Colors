using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElektrikEngelScript : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int amount = 1; // Karakterin alacağı hasar miktarı
    public string playerTag = "Player"; // Oyuncu karakterinin etiketi
    public string groundTag = "Ground"; // Zemin objesinin etiketi

    private Rigidbody2D rb;
    private Collider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        rb.isKinematic = true; // Başlangıçta kabloyu kinematik yap
        col.isTrigger = false; // Collider'ı başlangıçta trigger olarak ayarlama
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Herhangi bir obje ile çarpışma
        if (collision.gameObject.CompareTag(playerTag))
        {
            playerHealth.TakeDamage(amount);
            Debug.Log("Player damaged by electric cable!");
        }
        else
        {
            rb.isKinematic = false; // Kablocu dinamik yap
            col.isTrigger = true; // Collider'ı trigger yap
            Debug.Log("Electric cable grounded and became dynamic!");
        }
    }
}
