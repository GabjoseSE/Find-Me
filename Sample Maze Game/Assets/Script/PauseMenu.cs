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

    public void quit()
    {
        SceneManager.LoadSceneAsync(2);
        Time.timeScale = 1;
    }

}
