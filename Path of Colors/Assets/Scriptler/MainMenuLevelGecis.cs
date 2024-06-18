using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLevelGecis : MonoBehaviour
{
    // Oyunu başlatmak için kullanılacak metod
    public void PlayGame()
    {
        // İlk sahneyi yükler (örneğin, oyunun ilk seviyesi)
        SceneManager.LoadScene("Level 1");
    }

    // Oyunu kapatmak için kullanılacak metod
    public void QuitGame()
    {
        // Oyun oynanırken oyun penceresini kapatır
        Debug.Log("Quit!");
        Application.Quit();
    }

    // Belirtilen sahneyi yüklemek için kullanılacak metod
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}

