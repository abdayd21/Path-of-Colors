using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngelDusmesi : MonoBehaviour
{
    public Rigidbody2D targetRigidbody; // De�i�tirilecek Rigidbody2D bile�eni

    // Nesne, belirli bir Collider ile �arp��t���nda tetiklenecek olay
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �arp��ma yapan nesnenin "Player" etiketine sahip olup olmad���n� kontrol ederiz
        if (collision.CompareTag("Player"))
        {
            // Hedef Rigidbody'nin bodyType'�n� Kinematic'ten Dynamic'e de�i�tiririz
            targetRigidbody.bodyType = RigidbodyType2D.Dynamic;

            Destroy(gameObject, 2f);
        }
    }
}
