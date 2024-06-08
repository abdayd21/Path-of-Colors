using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Animator Isik_1;
    public Animator Isik_2;
    public KopruScript brindge;
    private bool playerInRange = false;
     Animator anim;
void Start()
{
    anim=GetComponent<Animator>();
}
    void Update()
    {
        // Eğer oyuncu düğme alanındaysa ve 'E' tuşuna basarsa
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            brindge.FlattenBridge();
            Isik_1.SetBool("Yesil",true);
            Isik_2.SetBool("Yesil",true);   

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
//bridge.FlattenBridge();
