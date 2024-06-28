using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class KapiAcilmaVeGorunurlukDegistirmeScript : MonoBehaviour
{  
    [Header("*Cameras")]
    public CinemachineVirtualCamera[] PlayerCams; // Oyuncunun kameraları
    public CinemachineVirtualCamera[] bosCameras; // Boş kameralar (Cinematic kameralar)

    [Header("*DoorAnim")]
    public Animator[] SolKapilar; // Sol kapılar
    public Animator[] SagKapilar; // Sağ kapılar

    [Header("*IsikAnim")]
    public Animator[] IsikAnim; // Işık animasyonları
     
    [Header("*Görünürlüğünü kontrol etmek istediğiniz objeler")]
    public GameObject[] targetObjects; // Görünürlüğünü kontrol etmek istediğiniz objeler

    [Header("*E tuşu görünümü")]
    public GameObject[] buttonE; // E tuşu görünümü

    [Header("*Kameranın geri dönme süresi")]
    public float cameraSwitchBackTime = 2.0f; // Kameranın geri dönme süresi

    [Header("*Animasyonun başlamadan önceki gecikme süresi")]
    public float animationDelay = 1.0f; // Animasyonun başlamadan önceki gecikme süresi

    [Header("*Hinge Joint2D Objeleri")]
    public HingeJoint2D[] hingeJoints; // HingeJoint2D bileşenine sahip objeler

    private bool playerInRange = false;
    private bool isSwitched = false; // Kameranın değiştirilip değiştirilmediğini kontrol eden bayrak
    private bool buttonPressed = false; // Düğmeye basıldığını kontrol eden bayrak
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        // Başlangıçta objelerin görünürlüğünü açık olarak ayarlıyoruz
        foreach (var obj in targetObjects)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
        foreach (var button in buttonE)
        {
            if (button != null)
            {
                button.SetActive(true);
            }
        }
    }

    void Update()
    {
        // Eğer oyuncu düğme alanındaysa ve 'E' tuşuna basarsa
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !buttonPressed)
        {
            StartCoroutine(PlayAnimationsWithDelay(animationDelay));

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

    // Animasyonları belirli bir gecikme süresi ile oynatmak ve hinge joints bileşenlerini devre dışı bırakmak için kullanılan metod
    private IEnumerator PlayAnimationsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (var anim in SolKapilar)
        {
            if (anim != null) // Null kontrolü
            {
                anim.SetBool("Open", true);
            }
        }
        foreach (var anim in SagKapilar)
        {
            if (anim != null) // Null kontrolü
            {
                anim.SetBool("Open", true);
            }
        }
        foreach (var anim in IsikAnim)
        {
            if (anim != null) // Null kontrolü
            {
                anim.SetBool("Yesil", true);
            }
        }

        // Objeleri aktif/pasif duruma getir
        foreach (var obj in targetObjects)
        {
            if (obj != null)
            {
                bool isActive = obj.activeSelf;
                obj.SetActive(!isActive);
            }
        }
        foreach (var button in buttonE)
        {
            if (button != null)
            {
                bool isActive = button.activeSelf;
                button.SetActive(!isActive);
            }
        }

        // HingeJoint2D bileşenlerini devre dışı bırak
        foreach (var hinge in hingeJoints)
        {
            if (hinge != null)
            {
                hinge.enabled = false;
            }
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
