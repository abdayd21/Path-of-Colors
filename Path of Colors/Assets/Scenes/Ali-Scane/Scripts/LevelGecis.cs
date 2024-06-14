using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGecks : MonoBehaviour
{
    private bool isTriggered = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;

            // Start scene transition
            Invoke("ChangeScene", 3f); // Delay scene transition by 3 seconds
        }
    }

    private void ChangeScene()
    {
        // Move to the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnDestroy()
    {
        // Cleanup actions when this object is destroyed
        // For example: Cancel invoke actions
        CancelInvoke();
    }
}
