using UnityEngine;
using System.Collections;

public class ControlSwitch : MonoBehaviour
{
    private GameObject player;
    private CharControl playerController;
    private SoulController[] soulControllers;
    private SoulController currentSoulController;
    private bool isSoulControlled = false;
    public float switchDistance = 5f;

    void Start()
    {
        Debug.Log("ControlSwitch script started.");

        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<CharControl>();
        }

        if (playerController == null)
        {
            Debug.LogError("CharacterController script is not found on the player.");
        }
        else
        {
            Debug.Log("CharacterController script found on the player.");
        }

        // Tüm soul objelerini bul
        soulControllers = FindObjectsOfType<SoulController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isSoulControlled)
        {
            Debug.Log("E key pressed, attempting to switch control.");

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
                Debug.Log("No soul object within range to switch control.");
            }
        }
        else if (isSoulControlled && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed, switching control back to player.");
            SwitchControlBack();
        }
    }

    IEnumerator SwitchControl(GameObject soulObject)
    {
        Debug.Log("Switching control to soul object.");

        // Karakterin kontrolünü kapat
        playerController.isControlled = false;
        playerController.enabled = false;

        // Soul nesnesinin kontrolünü aç
        SoulController soulController = soulObject.GetComponent<SoulController>();
        soulController.isControlled = true;
        soulController.enabled = true;

        currentSoulController = soulController;
        isSoulControlled = true;

        yield return null;
    }

    void SwitchControlBack()
    {
        if (currentSoulController != null)
        {
            Debug.Log("Switching control back to player.");

            // Soul nesnesinin kontrolünü kapat
            currentSoulController.isControlled = false;
            currentSoulController.enabled = false;

            // Karakterin kontrolünü aç
            playerController.isControlled = true;
            playerController.enabled = true;

            isSoulControlled = false;
            currentSoulController = null;
        }
    }
}
