using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [Header("Movement Particle")]
    [SerializeField] ParticleSystem movementParticle;
    [Range(0,10)]
    [SerializeField] int occurAfterVlecity;

    [Range(0,0.2f)]
    [SerializeField] float dusutFormationPeriod;
    [SerializeField] Rigidbody2D rb;

    float counter;
    bool isOnGround;

    [Header("")]
    [SerializeField] ParticleSystem fallParticle;

    private void Update()
    {
       counter += Time.deltaTime;

        if(isOnGround && Mathf.Abs(rb.velocity.x) > occurAfterVlecity)
        {
            if(counter > dusutFormationPeriod)
            {
                movementParticle.Play();
                counter = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int groundLayer = LayerMask.NameToLayer("Ground");

        if (collision.gameObject.layer == groundLayer)
        {
            fallParticle.Play();
            isOnGround = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        int groundLayer = LayerMask.NameToLayer("Ground");

        if (collision.gameObject.layer == groundLayer)
        {
            isOnGround = false;
        }
    }
}
