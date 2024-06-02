using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isControlled = true; // Kontrol alt�nda olup olmad���n� belirler

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
