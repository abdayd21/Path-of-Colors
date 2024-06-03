using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl2 : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

    private ButtonTrigger button1Trigger;
    private ButtonTrigger button2Trigger;
    private ButtonTrigger button3Trigger;
    public GameObject door;
    private BoxCollider2D doorCollider;
    private Animator anim;

    void Start()
    {
        button1Trigger = button1.GetComponent<ButtonTrigger>();
        button2Trigger = button2.GetComponent<ButtonTrigger>();
        button3Trigger = button3.GetComponent<ButtonTrigger>();
        doorCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (button1Trigger.isPressed && button2Trigger.isPressed && button3Trigger.isPressed)
        {
            OpenDoor();

        }
        else
        {
            CloseDoor();
            anim.SetBool("DoorOpen", false);
        }
    }

    void OpenDoor()
    {
        // Kapýyý açma kodunu buraya ekleyin (örneðin, kapýyý yukarý hareket ettirme)
        doorCollider.isTrigger = true;
        anim.SetBool("DoorOpen", true);
    }

    void CloseDoor()
    {
        // Kapýyý kapama kodunu buraya ekleyin (örneðin, kapýyý aþaðý hareket ettirme)
        doorCollider.isTrigger = false;
        anim.SetBool("DoorOpen", true);
    }
}