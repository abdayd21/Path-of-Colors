using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsansorButton1 : MonoBehaviour
{
public AsansorScript platformController; // Reference to the PlatformController script
 // Reference to the PlatformController script

    void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            platformController.MovePlatform(); // Call the MovePlatform method when the T key is pressed
        }
    }
}