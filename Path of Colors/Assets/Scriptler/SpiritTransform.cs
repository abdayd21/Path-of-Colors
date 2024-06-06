using UnityEngine;

public class SpiritTransfer : MonoBehaviour
{
    private GameObject currentTarget;
    private GameObject previousControlledObject;
    private bool isTransferring = false;
    public float transferDuration = 3f; // Ruh geçiþi süresi (saniye)

    void Update()
    {
        // E tuþuna basýldýðýnda ve bir hedef varsa ve ruh aktarýmý yapýlmýyorsa
        if (Input.GetKeyDown(KeyCode.E) && currentTarget != null && !isTransferring)
        {
            TransferControl();
        }

        // Eðer bir hedef varsa ve ruh aktarýmý yapýlmýyorsa ve "E" tuþuna basýldýðýnda
        // karakterin kontrolünü geri almak için "E" tuþuna basýlmýþsa
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

        // Pozisyonlarý deðiþtir
        Vector3 tempPosition = previousControlledObject.transform.position;
        previousControlledObject.transform.position = currentTarget.transform.position;
        currentTarget.transform.position = tempPosition;

        // Rigidbody2D bileþenlerini al
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

        // BoxCollider2D bileþenlerini al
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

        // Rigidbody2D bileþenlerini güncelle
        previousRb.isKinematic = true; // Önceki kontrol edilen nesneyi statik yap
        newRb.isKinematic = false; // Yeni kontrol edilen nesnede fiziði etkinleþtir
        newRb.bodyType = RigidbodyType2D.Dynamic; // Yeni kontrol edilen nesneyi dinamik yap

        // BoxCollider2D bileþenlerini güncelle
        previousCollider.isTrigger = true; // Önceki kontrol edilen nesneyi çarpýþmasýz yap
        newCollider.isTrigger = false; // Yeni kontrol edilen nesnede çarpýþmalarý etkinleþtir

        // Ruh geçiþini gerçekleþtir
        isTransferring = true;
        Invoke("ReturnControl", transferDuration); // Belirli bir süre sonra ruhu geri döndür
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

            // Karakterin kontrolünü geri al
            ObjeControl.currentControlledObject = gameObject;

            BoxCollider2D objCollider = GetComponent<BoxCollider2D>();
            if (objCollider != null)
            {
                objCollider.isTrigger = false;
            }
        }
    }
}