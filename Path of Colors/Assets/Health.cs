using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için gerekli

public class Health : MonoBehaviour
{
    public float maxHealth = 100f; // Maksimum saðlýk
    public float currentHealth; // Mevcut saðlýk

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth; // Saðlýðý maksimum saðlýk deðeri ile baþlat
    }

    /// <summary>
    /// Saðlýk puanlarýný azaltýr
    /// </summary>
    /// <param name="amount">Azaltýlacak saðlýk miktarý</param>
    public void TakeDamage(float amount)
    {
        currentHealth -= amount; // Saðlýðý azalt
        if (currentHealth <= 0)
        {
            StartCoroutine(Die()); // Saðlýk sýfýrýn altýna düþerse öl ve sahneyi yeniden baþlat
        }
    }

    /// <summary>
    /// Ölüm iþlemlerini gerçekleþtirir ve sahneyi yeniden baþlatýr
    /// </summary>
    IEnumerator Die()
    {
        // Ölüm iþlemleri, örneðin oyunu sonlandýrma, karakteri devre dýþý býrakma vb.
        Debug.Log("Player has died.");
        //Time.timeScale = 0f; // Oyunu durdur
        yield return new WaitForSecondsRealtime(1f); // 1 saniye bekle (gerçek zaman)
        //Time.timeScale = 1f; // Oyunu devam ettir
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Geçerli sahneyi yeniden yükle
    }
}
