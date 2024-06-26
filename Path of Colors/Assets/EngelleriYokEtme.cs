using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngelleriYokEtme : MonoBehaviour
{
    public Animator buttonAnimator; // Butonun animatör bileşeni
    public static readonly string ANIMATION_ACTIVE = "active"; // Çalacak animasyonun adı (boolean parametre)
    public GameObject[] objectsToDestroy; // Yok edilecek objelerin listesi
    private bool playerInRange = false; // Oyuncunun butonun alanında olup olmadığını kontrol eder

    void Update()
    {
        // Oyuncu butonun alanında ve E tuşuna basıldığında
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Animasyonu çalıştır
            if (buttonAnimator != null)
            {
                buttonAnimator.SetBool(ANIMATION_ACTIVE, true);
            }

            // Belirtilen objeleri 0.5 saniye sonra yok et
            StartCoroutine(DestroyObjectsWithDelay(0.5f));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Oyuncu butonun alanına girdiğinde
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Oyuncu butonun alanından çıktığında
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    // Belirtilen objeleri gecikmeli olarak yok eden coroutine
    private IEnumerator DestroyObjectsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }
}