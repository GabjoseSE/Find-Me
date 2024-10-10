using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;


    public void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;

    }

    public void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;

    }

    public void quitGame()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1;
    }

}
