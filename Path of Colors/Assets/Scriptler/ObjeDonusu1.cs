using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjeDonusu1 : MonoBehaviour
{
   // Dönüş hızı
    public float rotationSpeed = 100f;

    // Dönüş durma süresi
    public float pauseDuration = 1f;

    private bool isRotating = true;

    void Start()
    {
        // Objeyi döndürmeye başlayın
        StartRotation();
    }

    void Update()
    {
        // Eğer dönme durumu aktifse, objeyi döndürün
        if (isRotating)
        {
            // Objeyi saat yönünde döndür
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }

    // Dönme işlemini başlatan fonksiyon
    void StartRotation()
    {
        isRotating = true;
        Invoke("StopRotation", 180f / Mathf.Abs(rotationSpeed)); // 180 derece tam tur için dönme süresi
    }

    // Dönme işlemini durduran fonksiyon
    void StopRotation()
    {
        isRotating = false;
        Invoke("StartRotation", pauseDuration); // Durma süresinden sonra tekrar dönme işlemini başlat
    }
}