using UnityEngine;

public class SwingingPlatform : MonoBehaviour
{
    public Rigidbody2D platformRigidbody; // Platformun Rigidbody2D bileşeni
    public float forceMultiplier = 10f; // Kuvvet çarpanı, platformun ne kadar hızlı sallanacağını belirler
    private bool isPlayerOnPlatform = false;
    private Rigidbody2D playerRigidbody;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
            playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
            playerRigidbody = null;
        }
    }

    private void FixedUpdate()
    {
        if (isPlayerOnPlatform && playerRigidbody != null)
        {
            // Karakterin yatay hızını al
            float playerHorizontalVelocity = playerRigidbody.velocity.x;

            // Platforma yatay kuvvet uygula
            platformRigidbody.AddForce(new Vector2(playerHorizontalVelocity * forceMultiplier, 0));
        }
    }
}
