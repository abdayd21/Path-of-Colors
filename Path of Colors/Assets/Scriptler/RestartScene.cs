using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // R tuþuna basýldýðýnda
        {
            RestartCurrentScene(); // Sahneyi yeniden baþlat
        }
    }

    void RestartCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene(); // Aktif sahneyi al
        SceneManager.LoadScene(currentScene.name); // Sahneyi yeniden yükle
    }
}
