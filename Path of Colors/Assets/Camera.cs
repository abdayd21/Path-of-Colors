using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Unity motorunda iki Cinemachine sanal kamera tan�ml�yoruz.
    public Cinemachine.CinemachineVirtualCamera PlayerCam; // Oyuncunun kameras�
    public Cinemachine.CinemachineVirtualCamera bosCamera; // Bo� kameran�n (Cinematic kamera) referans�

    private bool isSwitched = false; // Kameran�n de�i�tirilip de�i�tirilmedi�ini kontrol eden bayrak

    // Nesne, belirli bir Collider ile �arp��t���nda tetiklenecek olay
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �arp��ma yapan nesnenin "Player" etiketine sahip olup olmad���n� kontrol ederiz
        if (collision.CompareTag("Player"))
        {
            // Kameran�n de�i�tirildi�ini belirtiriz
            isSwitched = true;
            // Kameray� de�i�tiririz
            SwitchCamera();
        }
    }

    // Kameray� de�i�tirmek i�in kullan�lan metod
    private void SwitchCamera()
    {
        // Oyuncunun kameras�n�n �nceli�ini 10 yapar�z (daha d���k �ncelik)
        PlayerCam.Priority = 10;
        // Bo� kameran�n �nceli�ini 20 yapar�z (daha y�ksek �ncelik)
        bosCamera.Priority = 20;
    }
}
