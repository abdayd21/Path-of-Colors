using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KapiAcilmaScript : MonoBehaviour
{
    public Animator SolKapi;
    public Animator SagKapi;
    public Animator IsikAnim;
    public Animator IsikAnim1;
    public Animator IsikAnim2;
    private bool playerInRange = false;
    Animator anim;

void Start () 
{
    anim=GetComponent<Animator>();
}

    void Update()
    {
        // Eğer oyuncu düğme alanındaysa ve 'E' tuşuna basarsa
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SolKapi.SetBool("Open", true);
            SagKapi.SetBool("Open",true);
            IsikAnim.SetBool("Yesil",true);    
            IsikAnim1.SetBool("Yesil",true);
            IsikAnim2.SetBool("Yesil",true);        
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;   
        }
    }
}

