using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void difficulty() //it will go to difficulty tab
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void option()//it will go to option tab
    {
        SceneManager.LoadSceneAsync(4);
    }
    public void about()//it will go to about tab
    {
        SceneManager.LoadSceneAsync(5);
    }
    public void quit()//it will exit application
    {
        Application.Quit();
    }
    public void back()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void login()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void signup()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
