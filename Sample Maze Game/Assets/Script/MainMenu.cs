using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private string[] easyLevelMaps = { "easylevelmap1", "easylevelmap2", "easylevelmap3", "easylevelmap4", "easylevelmap5" };
    private string[] meanLevelMaps = { "meanlevelmap1", "meanlevelmap2", "meanlevelmap3", "meanlevelmap4", "meanlevelmap5" };
    private string[] hardLevelMaps = { "hardlevelmap1", "hardlevelmap2", "hardlevelmap3", "hardlevelmap4", "hardlevelmap5" };

    public GameObject loadingScreen; // Loading screen UI reference
    
    public void PauseQuitGame()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1;  
    }

    public void ExitApplication()
    {
        Debug.Log("Quit button Clicked");
        Application.Quit();
    }

    public void MainMenuPage()
    {
        SceneManager.LoadSceneAsync(0);
        Debug.Log("Go to MainMenu");
    }

    public void SignUpPage()
    {
        SceneManager.LoadSceneAsync(1);
        Debug.Log("Go to SignUp");
    }

    public void LoadEasyLevel()
    {
        int randomMapIndex = Random.Range(0, easyLevelMaps.Length);
        StartCoroutine(LoadSceneAsync(easyLevelMaps[randomMapIndex]));
    }

    public void LoadMeanLevel()
    {
        int randomMapIndex = Random.Range(0, meanLevelMaps.Length);
        StartCoroutine(LoadSceneAsync(meanLevelMaps[randomMapIndex]));
    }

    public void LoadHardLevel()
    {
        int randomMapIndex = Random.Range(0, hardLevelMaps.Length);
        StartCoroutine(LoadSceneAsync(hardLevelMaps[randomMapIndex]));
    }

    public void Logout()
    {
        PlayerPrefs.SetInt("isLoggedIn", 0); // Set login state to not logged in
        PlayerPrefs.DeleteKey("LoggedInUser");
        PlayerPrefs.DeleteKey("freeze");
        PlayerPrefs.DeleteKey("invisibility");
        PlayerPrefs.DeleteKey("navigation");
        PlayerPrefs.DeleteKey("speedup");
        Debug.Log("Logout, deleted username");
        SceneManager.LoadSceneAsync(0); // Replace 0 with the index of your login scene
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Activate the loading screen
        loadingScreen.SetActive(true);

        // Begin loading the scene asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // While the scene is loading, update the progress bar
        while (!operation.isDone)
        {
            // Optionally, you can display the loading progress (ranges from 0.0 to 1.0)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            

            // Yield the frame to allow loading to proceed
            yield return null;
        }

        // Deactivate the loading screen once the scene is loaded
        loadingScreen.SetActive(false);
    }
}
