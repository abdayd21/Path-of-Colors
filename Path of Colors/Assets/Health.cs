using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahne y�netimi i�in gerekli

public class Health : MonoBehaviour
{
    public float maxHealth = 100f; // Maksimum sa�l�k
    public float currentHealth; // Mevcut sa�l�k

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth; // Sa�l��� maksimum sa�l�k de�eri ile ba�lat
    }

    /// <summary>
    /// Sa�l�k puanlar�n� azalt�r
    /// </summary>
    /// <param name="amount">Azalt�lacak sa�l�k miktar�</param>
    public void TakeDamage(float amount)
    {
        currentHealth -= amount; // Sa�l��� azalt
        if (currentHealth <= 0)
        {
            StartCoroutine(Die()); // Sa�l�k s�f�r�n alt�na d��erse �l ve sahneyi yeniden ba�lat
        }
    }

    /// <summary>
    /// �l�m i�lemlerini ger�ekle�tirir ve sahneyi yeniden ba�lat�r
    /// </summary>
    IEnumerator Die()
    {
        // �l�m i�lemleri, �rne�in oyunu sonland�rma, karakteri devre d��� b�rakma vb.
        Debug.Log("Player has died.");
        //Time.timeScale = 0f; // Oyunu durdur
        yield return new WaitForSecondsRealtime(1f); // 1 saniye bekle (ger�ek zaman)
        //Time.timeScale = 1f; // Oyunu devam ettir
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Ge�erli sahneyi yeniden y�kle
    }
}
