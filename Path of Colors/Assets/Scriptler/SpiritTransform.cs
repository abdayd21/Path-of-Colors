using UnityEngine;

public class SpiritTransfer : MonoBehaviour
{
    private GameObject currentTarget;
    private GameObject previousControlledObject;
    private bool isTransferring = false;
    public float transferDuration = 3f; // Ruh ge�i�i s�resi (saniye)

    void Update()
    {
        // E tu�una bas�ld���nda ve bir hedef varsa ve ruh aktar�m� yap�lm�yorsa
        if (Input.GetKeyDown(KeyCode.E) && currentTarget != null && !isTransferring)
        {
            TransferControl();
        }

        // E�er bir hedef varsa ve ruh aktar�m� yap�lm�yorsa ve "E" tu�una bas�ld���nda
        // karakterin kontrol�n� geri almak i�in "E" tu�una bas�lm��sa
        if (currentTarget != null && !isTransferring && Input.GetKeyDown(KeyCode.E))
        {
            ReturnControl();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Controllable"))
        {
            currentTarget = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Controllable"))
        {
            currentTarget = null;
        }
    }
    void TransferControl()
    {
        if (ObjeControl.currentControlledObject == null)
        {
            Debug.LogError("currentControlledObject is null.");
            return;
        }

        if (currentTarget == null)
        {
            Debug.LogError("currentTarget is null.");
            return;
        }

        previousControlledObject = ObjeControl.currentControlledObject;
        ObjeControl.currentControlledObject = currentTarget;

        // Pozisyonlar� de�i�tir
        Vector3 tempPosition = previousControlledObject.transform.position;
        previousControlledObject.transform.position = currentTarget.transform.position;
        currentTarget.transform.position = tempPosition;

        // Rigidbody2D bile�enlerini al
        Rigidbody2D previousRb = previousControlledObject.GetComponent<Rigidbody2D>();
        Rigidbody2D newRb = currentTarget.GetComponent<Rigidbody2D>();

        if (previousRb == null)
        {
            Debug.LogError("previousControlledObject does not have a Rigidbody2D component.");
            return;
        }

        if (newRb == null)
        {
            Debug.LogError("currentTarget does not have a Rigidbody2D component.");
            return;
        }

        // BoxCollider2D bile�enlerini al
        BoxCollider2D previousCollider = previousControlledObject.GetComponent<BoxCollider2D>();
        BoxCollider2D newCollider = currentTarget.GetComponent<BoxCollider2D>();

        if (previousCollider == null)
        {
            Debug.LogError("previousControlledObject does not have a BoxCollider2D component.");
            return;
        }

        if (newCollider == null)
        {
            Debug.LogError("currentTarget does not have a BoxCollider2D component.");
            return;
        }

        // Rigidbody2D bile�enlerini g�ncelle
        previousRb.isKinematic = true; // �nceki kontrol edilen nesneyi statik yap
        newRb.isKinematic = false; // Yeni kontrol edilen nesnede fizi�i etkinle�tir
        newRb.bodyType = RigidbodyType2D.Dynamic; // Yeni kontrol edilen nesneyi dinamik yap

        // BoxCollider2D bile�enlerini g�ncelle
        previousCollider.isTrigger = true; // �nceki kontrol edilen nesneyi �arp��mas�z yap
        newCollider.isTrigger = false; // Yeni kontrol edilen nesnede �arp��malar� etkinle�tir

        // Ruh ge�i�ini ger�ekle�tir
        isTransferring = true;
        Invoke("ReturnControl", transferDuration); // Belirli bir s�re sonra ruhu geri d�nd�r
    }

    void ReturnControl()
    {
        if (previousControlledObject != null)
        {
            CharControl.currentControlledObject = previousControlledObject;
            previousControlledObject.GetComponent<Rigidbody2D>().isKinematic = false;
            previousControlledObject.GetComponent<BoxCollider2D>().isTrigger = false;
            previousControlledObject = null;
            isTransferring = false;

            // Karakterin kontrol�n� geri al
            ObjeControl.currentControlledObject = gameObject;

            BoxCollider2D objCollider = GetComponent<BoxCollider2D>();
            if (objCollider != null)
            {
                objCollider.isTrigger = false;
            }
        }
    }
}