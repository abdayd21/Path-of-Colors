using UnityEngine;
using System.Collections;

public class ControlSwitch : MonoBehaviour
{
    private GameObject player;
    private CharControl playerController;
    private SoulController currentSoulController;
    private bool canSwitch = false;

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
    }

    void Update()
    {
        if (canSwitch && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed, attempting to switch control.");
            StartCoroutine(SwitchControl(currentSoulController.gameObject));
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed, but cannot switch control.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D called with object: " + other.name);

        if (other.CompareTag("soul"))
        {
            Debug.Log("Entered trigger with a soul object.");
            currentSoulController = other.GetComponent<SoulController>();
            canSwitch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D called with object: " + other.name);

        if (other.CompareTag("soul"))
        {
            Debug.Log("Exited trigger with a soul object.");
            canSwitch = false;
            currentSoulController = null;
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

        yield return new WaitForSeconds(5f);

        Debug.Log("Switching control back to player.");

        // Soul nesnesinin kontrolünü kapat
        soulController.isControlled = false;
        soulController.enabled = false;

        // Karakterin kontrolünü aç
        playerController.isControlled = true;
        playerController.enabled = true;
    }
}
