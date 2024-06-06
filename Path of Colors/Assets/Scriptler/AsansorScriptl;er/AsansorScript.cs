using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsansorScript : MonoBehaviour
{
    // Asansörün hedef noktası
    public Transform targetPosition;
    
    // Asansörün başlangıç noktası
    private Vector3 startPosition;

    // Asansörün hareket hızı
    public float speed = 2f;

    // Asansörün hareket edip etmediğini kontrol eden değişken
    private bool isMoving = false;

    // Hedef pozisyonu kontrol eden değişken
    private bool movingToTarget = true;

    void Start()
    {
        // Başlangıç pozisyonunu kaydet
        startPosition = transform.position;
    }

    void Update()
    {
        // Asansör hareket ediyorsa
        if (isMoving)
        {
            Vector3 target = movingToTarget ? targetPosition.position : startPosition;
            // Asansörü hedef noktaya doğru hareket ettir
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            // Asansör hedef noktaya ulaştıysa durdur
            if (transform.position == target)
            {
                isMoving = false;
            }
        }
    }

    // Asansörü hareket ettirmeye başlatan fonksiyon
    public void StartMoving()
    {
        isMoving = true;
        movingToTarget = !movingToTarget;
    }
}
