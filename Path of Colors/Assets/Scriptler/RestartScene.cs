using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // R tu�una bas�ld���nda
        {
            RestartCurrentScene(); // Sahneyi yeniden ba�lat
        }
    }

    void RestartCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene(); // Aktif sahneyi al
        SceneManager.LoadScene(currentScene.name); // Sahneyi yeniden y�kle
    }
}
