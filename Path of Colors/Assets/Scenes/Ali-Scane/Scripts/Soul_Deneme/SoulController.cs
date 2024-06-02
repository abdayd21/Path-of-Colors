using UnityEngine;

public class SoulController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isControlled = false; // Kontrol altýnda olup olmadýðýný belirler

    void Update()
    {
        if (isControlled)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(moveX, moveY, 0f);
            transform.position += move * moveSpeed * Time.deltaTime;
        }
    }
}
