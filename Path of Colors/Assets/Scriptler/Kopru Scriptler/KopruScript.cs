using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KopruScript : MonoBehaviour
{
   private bool isFlat = false;
    private Quaternion targetRotation;
    
    // Dönüş hızı için bir public değişken ekleyin
    public float rotationSpeed = 2f;

    void Start()
    {
        // Başlangıç rotasyonu 60 derece olarak ayarlayın
        transform.rotation = Quaternion.Euler(0, 0, -30);
        targetRotation = transform.rotation;
    }

    void Update()
    {
        if (isFlat)
        {
            // Köprüyü hedef rotasyona doğru döndürün
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    // Köprüyü düz hale getiren fonksiyon
    public void FlattenBridge()
    {
        isFlat = true;
        targetRotation = Quaternion.Euler(0, 0, 0);
    }
}
