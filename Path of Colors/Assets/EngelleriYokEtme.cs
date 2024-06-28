using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AsansorVeEngelYokEtmeScript : MonoBehaviour
{
    [Header("Asansor Kontrol Scriptleri")]
    [Tooltip("Asansörleri kontrol eden scriptlerin referansları")]
    public AsansorScript[] platformControllers; // Asansörleri kontrol eden scriptlerin referansları
    
    [Header("Kameralar")]
    [Tooltip("Oyuncunun kameraları")]
    public CinemachineVirtualCamera[] PlayerCams; // Oyuncunun kameraları
    [Tooltip("Boş kameralar (Cinematic kameralar)")]
    public CinemachineVirtualCamera[] bosCameras; // Boş kameralar (Cinematic kameralar)

    [Header("Buton ve Animasyon")]
    [Tooltip("Butonun animatör bileşeni")]
    public Animator buttonAnimator; // Butonun animatör bileşeni
    [Tooltip("Çalacak animasyonun adı (boolean parametre)")]
    public static readonly string ANIMATION_ACTIVE = "active"; // Çalacak animasyonun adı (boolean parametre)

    [Header("Gecikme ve Süreler")]
    [Tooltip("Kameranın geri dönme süresi")]
    public float cameraSwitchBackTime = 2.0f; // Kameranın geri dönme süresi
    [Tooltip("Animasyonun başlamadan önceki gecikme süresi")]
    public float animationDelay = 1.0f; // Animasyonun başlamadan önceki gecikme süresi

    [Header("Yok Edilecek Objeler")]
    [Tooltip("Yok edilecek objelerin listesi")]
    public GameObject[] objectsToDestroy; // Yok edilecek objelerin listesi

    private bool playerInRange = false; // Oyuncunun butonun alanında olup olmadığını kontrol eder
    private bool isSwitched = false; // Kameranın değiştirilip değiştirilmediğini kontrol eden bayrak
    private bool buttonPressed = false; // Düğmeye basıldığını kontrol eden bayrak

    void Update()
    {
        // Eğer oyuncu butonun alanındaysa ve E tuşuna basıldıysa
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !buttonPressed)
        {
            // Tüm asansörleri hareket ettir
            foreach (var controller in platformControllers)
            {
                controller.MovePlatform();
            }

            // Animasyonu çalıştır
            if (buttonAnimator != null)
            {
                buttonAnimator.SetBool(ANIMATION_ACTIVE, true);
            }

            // Belirtilen objeleri 0.5 saniye sonra yok et
            StartCoroutine(DestroyObjectsWithDelay(0.5f));

            // Kamera değişimini başlat
            if (!isSwitched)
            {
                isSwitched = true;
                StartCoroutine(SwitchCameraWithDelay());
            }

            // Düğmeye basıldığını işaretle
            buttonPressed = true;
        }
    }

    // Oyuncu butonun alanına girdiğinde
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true; // Oyuncunun alan içinde olduğunu belirt
        }
    }

    // Oyuncu butonun alanından çıktığında
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false; // Oyuncunun alan dışında olduğunu belirt
        }
    }

    private IEnumerator DestroyObjectsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }

    // Kamerayı değiştirmek ve belirli bir süre sonra geri döndürmek için kullanılan metod
    private IEnumerator SwitchCameraWithDelay()
    {
        SwitchToBosCamera();

        // Belirtilen süre kadar bekleriz
        yield return new WaitForSeconds(cameraSwitchBackTime);

        SwitchToPlayerCamera();
    }

    // Boş kameraya geçiş yapmak için kullanılan metod
    private void SwitchToBosCamera()
    {
        // Tüm PlayerCams kameralarının önceliğini 10 yaparız (daha düşük öncelik)
        foreach (var cam in PlayerCams)
        {
            if (cam != null) // Null kontrolü
            {
                cam.Priority = 10;
            }
        }
        // Tüm bosCameras kameralarının önceliğini 20 yaparız (daha yüksek öncelik)
        foreach (var cam in bosCameras)
        {
            if (cam != null) // Null kontrolü
            {
                cam.Priority = 20;
            }
        }
    }

    // Oyuncunun kamerasına geri dönmek için kullanılan metod
    private void SwitchToPlayerCamera()
    {
        // Tüm PlayerCams kameralarının önceliğini 20 yaparız (daha yüksek öncelik)
        foreach (var cam in PlayerCams)
        {
            if (cam != null) // Null kontrolü
            {
                cam.Priority = 20;
            }
        }
        // Tüm bosCameras kameralarının önceliğini 10 yaparız (daha düşük öncelik)
        foreach (var cam in bosCameras)
        {
            if (cam != null) // Null kontrolü
            {
                cam.Priority = 10;
            }
        }

        isSwitched = false; // Kamera geri döndüğünde bayrağı sıfırla
    }
}
