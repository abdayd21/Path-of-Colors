using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaraktedenPlatform : MonoBehaviour
{
    public float moveDistance = 5f; // Platformun hareket edece�i mesafe
    public float moveSpeed = 2f; // Platformun hareket h�z�

    private Vector3 originalPosition; // Platformun ba�lang�� pozisyonu
    private bool isMoving = false; // Platformun hareket edip etmedi�ini kontrol eden bayrak
    private bool returning = false; // Platformun geri d�n�p d�nmedi�ini kontrol eden bayrak

    private Transform playerTransform; // Oyuncunun Transform bile�eni

    private void Start()
    {
        // Platformun orijinal pozisyonunu kaydet
        originalPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �arp��ma yapan nesnenin "Player" etiketine sahip olup olmad���n� kontrol ederiz
        if (collision.gameObject.CompareTag("Player") && !isMoving)
        {
            // Platformu hareket ettirmeye ba�lar�z
            isMoving = true;
            returning = false;
            // Oyuncunun Transform bile�enini kaydeder ve platformun �ocu�u yapar�z
            playerTransform = collision.transform;
            playerTransform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �arp��ma sona erdi�inde oyuncuyu ba��ms�z hale getiririz
        if (collision.gameObject.CompareTag("Player") && playerTransform != null)
        {
            playerTransform.SetParent(null);
            playerTransform = null;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            // Hareket mesafesinin kat edilip edilmedi�ini kontrol et
            if (!returning && Vector3.Distance(transform.position, originalPosition) < moveDistance)
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
            else
            {
                // Hareket mesafesi tamamland���nda geri d�nmeye ba�la
                returning = true;
            }

            if (returning)
            {
                // Orijinal pozisyona geri d�n
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);

                // Orijinal pozisyona d�nd���nde hareketi durdur
                if (Vector3.Distance(transform.position, originalPosition) < 0.01f)
                {
                    isMoving = false;
                    returning = false;
                    transform.position = originalPosition;

                    // E�er oyuncu hala platformun �zerindeyse, onu ba��ms�z hale getiririz
                    if (playerTransform != null)
                    {
                        playerTransform.SetParent(null);
                        playerTransform = null;
                    }
                }
            }
        }
    }
}