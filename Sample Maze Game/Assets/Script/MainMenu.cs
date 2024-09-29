using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string[] easyLevelMaps = { "easylevelmap1", "easylevelmap2", "easylevelmap3", "easylevelmap4", "easylevelmap5" };
    public void difficulty() //it will go to difficulty tab
    {
        SceneManager.LoadSceneAsync(3);
        Debug.Log("Clicked start");
    }
    public void store()//it will go to option tab
    {
        SceneManager.LoadSceneAsync(4);
        Debug.Log("Clicked store");
    }
    public void about()//it will go to about tab
    {
        SceneManager.LoadSceneAsync(5);
        Debug.Log("Clicked about");
    }
    public void quit()//it will exit application
    {
        Application.Quit();
        Debug.Log("Clicked quit");
    }
    public void back()
    {
        SceneManager.LoadSceneAsync(2);
        Debug.Log("Clicked back");
    }
    public void login()
    {
        SceneManager.LoadSceneAsync(0);

    }
    public void signup()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void easy()
    {
        SceneManager.LoadSceneAsync(6);
    }
    public void option()
    {
        SceneManager.LoadSceneAsync(7);
    }
    public void LoadEasyLevel()
    {
        // Randomly select a map from the easyLevelMaps array
        int randomMapIndex = Random.Range(0, easyLevelMaps.Length);

        // Load the selected map scene
        SceneManager.LoadScene(easyLevelMaps[randomMapIndex]);
    }
}
