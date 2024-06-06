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
        col.isTrigger = true; // Collider'ı trigger olarak ayarla
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Oyuncu karakteriyle çarpışma
        if (collision.gameObject.CompareTag(playerTag))
        {
            playerHealth.TakeDamage(amount);
            // Örnek: collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Debug.Log("Player damaged by electric cable!");
        }
        // Zeminle çarpışma
        else if (collision.gameObject.CompareTag(groundTag))
        {
            rb.isKinematic = false; // Kablocu dinamik yap
            col.isTrigger = false; // Collider'ı trigger olmaktan çıkar
            Debug.Log("Electric cable grounded and became dynamic!");
        }
    }
}
