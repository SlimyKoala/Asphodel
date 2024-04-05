using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    [SerializeField] GameObject pauseMenuUI;
    

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    private void Start()
    {
        Resume();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }
}
