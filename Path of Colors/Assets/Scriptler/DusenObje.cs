using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusenObje : MonoBehaviour
{
    public Rigidbody2D targetRigidbody; // Değiştirilecek Rigidbody2D bileşeni

    // Nesne, belirli bir Collider ile çarpıştığında tetiklenecek olay
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Çarpışma yapan nesnenin "Player" etiketine sahip olup olmadığını kontrol ederiz
        if (collision.CompareTag("Player") || collision.CompareTag("soul"))
        {
            // Hedef Rigidbody'nin bodyType'ını Kinematic'ten Dynamic'e değiştiririz
            targetRigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
