using UnityEngine;

public class SoulController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isControlled = false; // Kontrol alt�nda olup olmad���n� belirler

    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (isControlled)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            Vector2 move = new Vector2(moveX, moveY);
            rb.position+= move * moveSpeed * Time.deltaTime;
        }
    }
}
