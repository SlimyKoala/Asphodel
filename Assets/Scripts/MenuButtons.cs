using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    private void Start()
    {
        TimerManager.active.AddTimer(1, null);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }


    public void StartTutorial()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
