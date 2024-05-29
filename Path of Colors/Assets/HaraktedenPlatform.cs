using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaraktedenPlatform : MonoBehaviour
{
    public float moveDistance = 5f; // Platformun hareket edeceði mesafe
    public float moveSpeed = 2f; // Platformun hareket hýzý

    private Vector3 originalPosition; // Platformun baþlangýç pozisyonu
    private bool isMoving = false; // Platformun hareket edip etmediðini kontrol eden bayrak
    private bool returning = false; // Platformun geri dönüp dönmediðini kontrol eden bayrak

    private Transform playerTransform; // Oyuncunun Transform bileþeni

    private void Start()
    {
        // Platformun orijinal pozisyonunu kaydet
        originalPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Çarpýþma yapan nesnenin "Player" etiketine sahip olup olmadýðýný kontrol ederiz
        if (collision.gameObject.CompareTag("Player") && !isMoving)
        {
            // Platformu hareket ettirmeye baþlarýz
            isMoving = true;
            returning = false;
            // Oyuncunun Transform bileþenini kaydeder ve platformun çocuðu yaparýz
            playerTransform = collision.transform;
            playerTransform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Çarpýþma sona erdiðinde oyuncuyu baðýmsýz hale getiririz
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
            // Hareket mesafesinin kat edilip edilmediðini kontrol et
            if (!returning && Vector3.Distance(transform.position, originalPosition) < moveDistance)
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
            else
            {
                // Hareket mesafesi tamamlandýðýnda geri dönmeye baþla
                returning = true;
            }

            if (returning)
            {
                // Orijinal pozisyona geri dön
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);

                // Orijinal pozisyona döndüðünde hareketi durdur
                if (Vector3.Distance(transform.position, originalPosition) < 0.01f)
                {
                    isMoving = false;
                    returning = false;
                    transform.position = originalPosition;

                    // Eðer oyuncu hala platformun üzerindeyse, onu baðýmsýz hale getiririz
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