using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjeControl : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    public static GameObject currentControlledObject;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentControlledObject = gameObject;
    }

    void Update()
    {
        if (currentControlledObject == gameObject)
        {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            moveVelocity = moveInput.normalized * speed;
        }
    }

    void FixedUpdate()
    {
        if (currentControlledObject == gameObject)
        {
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        }
    }
}


