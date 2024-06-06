using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KapiAcilmaAnimasyonScript : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
      anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           anim.SetBool("Open",true);
        }
    }
}
