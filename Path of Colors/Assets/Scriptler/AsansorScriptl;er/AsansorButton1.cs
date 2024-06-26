using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AsansorButton1 : MonoBehaviour
{
    public AsansorScript platformController; // Asansörü kontrol eden scriptin referansı
    private bool playerInRange = false; // Oyuncunun butonun alanında olup olmadığını kontrol eder
    public GameObject[] objectsToDestroy; // Yok edilecek objelerin listesi

    public CinemachineVirtualCamera[] PlayerCams;
    public CinemachineVirtualCamera[] bosCameras;

    public float cameraSwitchBackTime = 2.0f; // Kameranın geri dönme süresi
    private bool isSwitched = false; // Kameranın değiştirilip değiştirilmediğini kontrol eden bayrak

    void Update()
    {
        // Eğer oyuncu butonun alanındaysa ve T tuşuna basıldıysa
        if (playerInRange && Input.GetKeyDown(KeyCode.T))
        {
            platformController.MovePlatform(); // MovePlatform metodunu çağır
            // Belirtilen objeleri 0.5 saniye sonra yok et
            StartCoroutine(DestroyObjectsWithDelay(0.2f));

            // Kamera değişimini başlat
            if (!isSwitched)
            {
                isSwitched = true;
                StartCoroutine(SwitchCameraWithDelay());
            }
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
            cam.Priority = 10;
        }
        // Tüm bosCameras kameralarının önceliğini 20 yaparız (daha yüksek öncelik)
        foreach (var cam in bosCameras)
        {
            cam.Priority = 20;
        }
    }

    // Oyuncunun kamerasına geri dönmek için kullanılan metod
    private void SwitchToPlayerCamera()
    {
        // Tüm PlayerCams kameralarının önceliğini 20 yaparız (daha yüksek öncelik)
        foreach (var cam in PlayerCams)
        {
            cam.Priority = 20;
        }
        // Tüm bosCameras kameralarının önceliğini 10 yaparız (daha düşük öncelik)
        foreach (var cam in bosCameras)
        {
            cam.Priority = 10;
        }

        isSwitched = false; // Kamera geri döndüğünde bayrağı sıfırla
    }
}
