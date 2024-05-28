using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject door;
    [SerializeField] Collider2D boxCollider;
    private Vector2 vel;
    public float smoothTime;
    public bool isPickedUp;
    [SerializeField] Vector3 offset = new Vector3(0, 1f, 0);

    void Update()
    {
        if (isPickedUp)
        {
            transform.position = Vector2.SmoothDamp(transform.position, player.transform.position + offset, ref vel, smoothTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
            door.GetComponent<door>().UnlockDoor();
            boxCollider.enabled = false;
        }
    }
}
