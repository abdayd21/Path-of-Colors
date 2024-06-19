using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public GameMainMenu pauseMenu;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(PauseGame);
    }

    void PauseGame()
    {
        pauseMenu.PauseGame();
    }
}
