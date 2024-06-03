using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngelDusmesi : MonoBehaviour
{
    public Rigidbody2D targetRigidbody; // Deðiþtirilecek Rigidbody2D bileþeni

    // Nesne, belirli bir Collider ile çarpýþtýðýnda tetiklenecek olay
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Çarpýþma yapan nesnenin "Player" etiketine sahip olup olmadýðýný kontrol ederiz
        if (collision.CompareTag("Player"))
        {
            // Hedef Rigidbody'nin bodyType'ýný Kinematic'ten Dynamic'e deðiþtiririz
            targetRigidbody.bodyType = RigidbodyType2D.Dynamic;

            Destroy(gameObject, 2f);
        }
    }
}
