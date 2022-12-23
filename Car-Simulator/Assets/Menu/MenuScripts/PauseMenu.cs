using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePause = false;

    public GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePause)
            {
                Resume();
            }


            else
            {
                Paused();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GamePause = false;
    }

    void Paused()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GamePause = true;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
