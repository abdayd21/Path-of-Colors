using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class KapiAcilmaVeGorunurlukDegistirmeScript : MonoBehaviour
{  
    public CinemachineVirtualCamera PlayerCam; // Oyuncunun kamerası
    public CinemachineVirtualCamera bosCamera; // Boş kameranın (Cinematic kamera) referansı

    public Animator SolKapi;
    public Animator SagKapi;
    public Animator IsikAnim;
    public Animator IsikAnim1;
    public Animator IsikAnim2;

    public GameObject targetObject; // Görünürlüğünü kontrol etmek istediğiniz obje
    public GameObject buttonE; // E tusu Gorunum

    public float cameraSwitchBackTime = 2.0f; // Kameranın geri dönme süresi
    private bool playerInRange = false;
    private bool isSwitched = false; // Kameranın değiştirilip değiştirilmediğini kontrol eden bayrak
    private bool buttonPressed = false; // Düğmeye basıldığını kontrol eden bayrak
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        // Başlangıçta objenin görünürlüğünü açık olarak ayarlıyoruz
        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
        if (buttonE != null)
        {
            buttonE.SetActive(true);
        }
    }

    void Update()
    {
        // Eğer oyuncu düğme alanındaysa ve 'E' tuşuna basarsa
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !buttonPressed)
        {
            SolKapi.SetBool("Open", true);
            SagKapi.SetBool("Open", true);
            IsikAnim.SetBool("Yesil", true);
            IsikAnim1.SetBool("Yesil", true);
            IsikAnim2.SetBool("Yesil", true);

            // Objeyi aktif/pasif duruma getir
            if (targetObject != null)
            {
                bool isActive = targetObject.activeSelf;
                targetObject.SetActive(!isActive);
            }
            if (buttonE != null)
            {
            bool isActive = buttonE.activeSelf;
            buttonE.SetActive(!isActive);
            }

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
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
        // Oyuncunun kamerasının önceliğini 10 yaparız (daha düşük öncelik)
        PlayerCam.Priority = 10;
        // Boş kameranın önceliğini 20 yaparız (daha yüksek öncelik)
        bosCamera.Priority = 20;
    }

    // Oyuncunun kamerasına geri dönmek için kullanılan metod
    private void SwitchToPlayerCamera()
    {
        // Oyuncunun kamerasının önceliğini 20 yaparız (daha yüksek öncelik)
        PlayerCam.Priority = 20;
        // Boş kameranın önceliğini 10 yaparız (daha düşük öncelik)
        bosCamera.Priority = 10;

        isSwitched = false; // Kamera geri döndüğünde bayrağı sıfırla
    }
}
