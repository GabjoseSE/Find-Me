using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string[] easyLevelMaps = { "easylevelmap1", "easylevelmap2", "easylevelmap3","easylevelmap4","easylevelmap5" };
    private string[] meanLevelMaps = { "meanlevelmap1", "meanlevelmap2", "meanlevelmap3","meanlevelmap4","meanlevelmap5" };
    private string[] hardLevelMaps = { "hardlevelmap1", "hardlevelmap2", "hardlevelmap3","hardlevelmap4","hardlevelmap5" };
    
    
    public void QuitApplication()//it will exit application
    {
        Application.Quit();
        Debug.Log("Clicked quit");
    }
    public void MainMenuPage()
    {
        SceneManager.LoadSceneAsync(2);
        Debug.Log("Go to MainMenu");
    }
    
    public void SignUpPage()
    {
        SceneManager.LoadSceneAsync(1);
        Debug.Log("Go to SignUp");
    }
    
    public void LoadEasyLevel()
    {
        // Randomly select a map from the easyLevelMaps array
        int randomMapIndex = Random.Range(0, easyLevelMaps.Length);

        // Load the selected map scene
        SceneManager.LoadScene(easyLevelMaps[randomMapIndex]);
    }
    public void LoadMeanLevel()
    {
        // Randomly select a map from the easyLevelMaps array
        int randomMapIndex = Random.Range(0, meanLevelMaps.Length);

        // Load the selected map scene
        SceneManager.LoadScene(meanLevelMaps[randomMapIndex]);
    }
    public void LoadHardLevel()
    {
        // Randomly select a map from the easyLevelMaps array
        int randomMapIndex = Random.Range(0, hardLevelMaps.Length);

        // Load the selected map scene
        SceneManager.LoadScene(hardLevelMaps[randomMapIndex]);
    }
}
