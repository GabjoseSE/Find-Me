using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private string[] easyLevelMaps = { "easylevelmap1","easylevelmap2","easylevelmap3","easylevelmap4","easylevelmap5"};
    private string[] meanLevelMaps = { "meanlevelmap2", "meanlevelmap3", "meanlevelmap4", "meanlevel map 5" };
    private string[] hardLevelMaps = { "hardlevelmap1", "hardlevelmap2", "hardlevelmap3"};

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
    // Activate the loading screen before starting the scene load
    loadingScreen.SetActive(true);

    // Begin loading the scene asynchronously
    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

    // Ensure the scene doesn't activate immediately when loading completes
    operation.allowSceneActivation = false;

    // Optionally: You can show the loading progress here if desired
    while (operation.progress < 0.9f) // Scene is considered loaded at 0.9f progress
    {
        // Optionally, update a progress bar here (if you have one)
        yield return null;
    }

    // Once the scene is fully loaded (reaches 90%), wait for a brief moment
    yield return new WaitForSeconds(1); // Optional: extra time to keep loading screen visible

    // Allow the scene to activate (this will switch to the new scene)
    operation.allowSceneActivation = true;

    // Wait for one more frame to ensure the new scene has fully activated
    yield return null;

    // Deactivate the loading screen after the new scene has fully activated
    loadingScreen.SetActive(false);
}

}
