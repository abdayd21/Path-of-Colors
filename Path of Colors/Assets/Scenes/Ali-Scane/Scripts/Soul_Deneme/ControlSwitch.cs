using UnityEngine;
using System.Collections;

public class ControlSwitch : MonoBehaviour
{
    private GameObject player;
    private CharacterController2D playerController;
    private Animator playerAnimator;
    private SoulController[] soulControllers;
    private SoulController currentSoulController;
    private bool isSoulControlled = false;
    public float switchDistance = 5f;

    void Start()
    {
        Debug.Log("ControlSwitch scripti baþlatýldý.");

        // Hiyerarþide 'Player' isimli GameObject'i bul
        player = GameObject.Find("Player");
        if (player != null)
        {
            // CharacterController2D bileþenini player nesnesinde arayýn
            playerController = player.GetComponent<CharacterController2D>();

            // Animator bileþenini child nesnesinde arayýn
            playerAnimator = player.GetComponentInChildren<Animator>();
        }

        if (playerController == null)
        {
            Debug.LogError("CharacterController scripti player nesnesinde bulunamadý.");
        }
        else
        {
            Debug.Log("CharacterController scripti player nesnesinde bulundu.");
        }

        if (playerAnimator == null)
        {
            Debug.LogError("Animator scripti player child nesnesinde bulunamadý.");
        }
        else
        {
            Debug.Log("Animator scripti player child nesnesinde bulundu.");
        }

        // Tüm soul objelerini bul
        soulControllers = FindObjectsOfType<SoulController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isSoulControlled)
        {
            Debug.Log("E tuþuna basýldý, kontrol deðiþimi denemesi yapýlýyor.");

            SoulController closestSoul = null;
            float closestDistance = switchDistance;

            // En yakýn soul nesnesini bul
            foreach (SoulController soul in soulControllers)
            {
                float distance = Vector3.Distance(player.transform.position, soul.transform.position);
                if (distance < closestDistance)
                {
                    closestSoul = soul;
                    closestDistance = distance;
                }
            }

            if (closestSoul != null)
            {
                StartCoroutine(SwitchControl(closestSoul.gameObject));
            }
            else
            {
                Debug.Log("Kontrol deðiþimi için menzil içinde soul nesnesi yok.");
            }
        }
        else if (isSoulControlled && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E tuþuna basýldý, kontrol tekrar player'a geçiriliyor.");
            SwitchControlBack();
        }
    }

    IEnumerator SwitchControl(GameObject soulObject)
    {
        Debug.Log("Kontrol soul nesnesine geçiriliyor.");

        if (playerController != null)
        {
            // Player kontrolünü kapat
            playerController.isControlled = false;
            playerController.enabled = false;
        }
        else
        {
            Debug.LogError("PlayerController is null in SwitchControl.");
        }

        // Animator kontrolünü kapat
        if (playerAnimator != null)
        {
            playerAnimator.enabled = false;
        }
        else
        {
            Debug.LogError("PlayerAnimator is null in SwitchControl.");
        }

        // Soul nesnesinin kontrolünü aç
        SoulController soulController = soulObject.GetComponent<SoulController>();
        if (soulController != null)
        {
            soulController.isControlled = true;
            soulController.enabled = true;

            currentSoulController = soulController;
            isSoulControlled = true;
        }
        else
        {
            Debug.LogError("SoulController is null on the target soul object.");
        }

        yield return null;
    }

    void SwitchControlBack()
    {
        if (currentSoulController != null)
        {
            Debug.Log("Kontrol tekrar player'a geçiriliyor.");

            // Soul nesnesinin kontrolünü kapat
            currentSoulController.isControlled = false;
            currentSoulController.enabled = false;

            // Player kontrolünü aç
            if (playerController != null)
            {
                playerController.isControlled = true;
                playerController.enabled = true;
            }
            else
            {
                Debug.LogError("PlayerController is null in SwitchControlBack.");
            }

            // Animator kontrolünü aç
            if (playerAnimator != null)
            {
                playerAnimator.enabled = true;
            }
            else
            {
                Debug.LogError("PlayerAnimator is null in SwitchControlBack.");
            }

            isSoulControlled = false;
            currentSoulController = null;
        }
        else
        {
            Debug.LogError("CurrentSoulController is null in SwitchControlBack.");
        }
    }
}
