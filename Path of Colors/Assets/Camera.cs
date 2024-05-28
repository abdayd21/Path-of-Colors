using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Unity motorunda iki Cinemachine sanal kamera tanýmlýyoruz.
    public Cinemachine.CinemachineVirtualCamera PlayerCam; // Oyuncunun kamerasý
    public Cinemachine.CinemachineVirtualCamera bosCamera; // Boþ kameranýn (Cinematic kamera) referansý

    private bool isSwitched = false; // Kameranýn deðiþtirilip deðiþtirilmediðini kontrol eden bayrak

    // Nesne, belirli bir Collider ile çarpýþtýðýnda tetiklenecek olay
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Çarpýþma yapan nesnenin "Player" etiketine sahip olup olmadýðýný kontrol ederiz
        if (collision.CompareTag("Player"))
        {
            // Kameranýn deðiþtirildiðini belirtiriz
            isSwitched = true;
            // Kamerayý deðiþtiririz
            SwitchCamera();
        }
    }

    // Kamerayý deðiþtirmek için kullanýlan metod
    private void SwitchCamera()
    {
        // Oyuncunun kamerasýnýn önceliðini 10 yaparýz (daha düþük öncelik)
        PlayerCam.Priority = 10;
        // Boþ kameranýn önceliðini 20 yaparýz (daha yüksek öncelik)
        bosCamera.Priority = 20;
    }
}
