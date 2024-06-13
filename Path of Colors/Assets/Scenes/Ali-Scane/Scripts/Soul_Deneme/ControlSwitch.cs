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
        Debug.Log("ControlSwitch scripti ba�lat�ld�.");

        // Hiyerar�ide 'Player' isimli GameObject'i bul
        player = GameObject.Find("Player");
        if (player != null)
        {
            // CharacterController2D bile�enini player nesnesinde aray�n
            playerController = player.GetComponent<CharacterController2D>();

            // Animator bile�enini child nesnesinde aray�n
            playerAnimator = player.GetComponentInChildren<Animator>();
        }

        if (playerController == null)
        {
            Debug.LogError("CharacterController scripti player nesnesinde bulunamad�.");
        }
        else
        {
            Debug.Log("CharacterController scripti player nesnesinde bulundu.");
        }

        if (playerAnimator == null)
        {
            Debug.LogError("Animator scripti player child nesnesinde bulunamad�.");
        }
        else
        {
            Debug.Log("Animator scripti player child nesnesinde bulundu.");
        }

        // T�m soul objelerini bul
        soulControllers = FindObjectsOfType<SoulController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isSoulControlled)
        {
            Debug.Log("E tu�una bas�ld�, kontrol de�i�imi denemesi yap�l�yor.");

            SoulController closestSoul = null;
            float closestDistance = switchDistance;

            // En yak�n soul nesnesini bul
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
                Debug.Log("Kontrol de�i�imi i�in menzil i�inde soul nesnesi yok.");
            }
        }
        else if (isSoulControlled && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E tu�una bas�ld�, kontrol tekrar player'a ge�iriliyor.");
            SwitchControlBack();
        }
    }

    IEnumerator SwitchControl(GameObject soulObject)
    {
        Debug.Log("Kontrol soul nesnesine ge�iriliyor.");

        if (playerController != null)
        {
            // Player kontrol�n� kapat
            playerController.isControlled = false;
            playerController.enabled = false;
        }
        else
        {
            Debug.LogError("PlayerController is null in SwitchControl.");
        }

        // Animator kontrol�n� kapat
        if (playerAnimator != null)
        {
            playerAnimator.enabled = false;
        }
        else
        {
            Debug.LogError("PlayerAnimator is null in SwitchControl.");
        }

        // Soul nesnesinin kontrol�n� a�
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
            Debug.Log("Kontrol tekrar player'a ge�iriliyor.");

            // Soul nesnesinin kontrol�n� kapat
            currentSoulController.isControlled = false;
            currentSoulController.enabled = false;

            // Player kontrol�n� a�
            if (playerController != null)
            {
                playerController.isControlled = true;
                playerController.enabled = true;
            }
            else
            {
                Debug.LogError("PlayerController is null in SwitchControlBack.");
            }

            // Animator kontrol�n� a�
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
