using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMainMenu : MonoBehaviour
{
     public GameObject pauseMenuPanel; // Pause menü paneli
    public Button resumeButton; // Resume butonu
    public Button settingsButton; // Settings butonu
    public Button quitButton; // Quit butonu

    private bool isPaused = false;

    void Start()
    {
        pauseMenuPanel.SetActive(false); // Başlangıçta pause menü panelini kapalı yap
        resumeButton.onClick.AddListener(ResumeGame); // Resume butonuna tıklama olayını ekle
        settingsButton.onClick.AddListener(OpenSettings); // Settings butonuna tıklama olayını ekle
        quitButton.onClick.AddListener(QuitToMainMenu); // Quit butonuna tıklama olayını ekle
    }

    void Update()
    {
        // Escape tuşuna basıldığında pause menüyü aç/kapa
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Oyunu durdurmak için kullanılan metod
    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; // Oyun zamanını durdur
        isPaused = true;
    }

    // Oyunu devam ettirmek için kullanılan metod
    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f; // Oyun zamanını devam ettir
        isPaused = false;
    }

    // Ayarlar menüsünü açmak için kullanılan metod (şu anlık boş)
    public void OpenSettings()
    {
        // Ayarlar menüsü işlemleri buraya eklenebilir
        Debug.Log("Settings Menu Opened");
    }

    // Ana menüye dönmek için kullanılan metod
    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Oyun zamanını normal yap
        SceneManager.LoadScene("MainMenu"); // Ana menü sahnesini yükle
    }
}
