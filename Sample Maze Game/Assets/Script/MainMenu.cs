using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string[] easyLevelMaps = { "easylevelmap1", "easylevelmap2", "easylevelmap3", "easylevelmap4", "easylevelmap5" };
    private string[] meanLevelMaps = { "meanlevelmap1", "meanlevelmap2", "meanlevelmap3", "meanlevelmap4", "meanlevelmap5" };
    private string[] hardLevelMaps = { "hardlevelmap1", "hardlevelmap2", "hardlevelmap3", "hardlevelmap4", "hardlevelmap5" };

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
        SceneManager.LoadScene(easyLevelMaps[randomMapIndex]);
    }

    public void LoadMeanLevel()
    {
        int randomMapIndex = Random.Range(0, meanLevelMaps.Length);
        SceneManager.LoadScene(meanLevelMaps[randomMapIndex]);
    }

    public void LoadHardLevel()
    {
        int randomMapIndex = Random.Range(0, hardLevelMaps.Length);
        SceneManager.LoadScene(hardLevelMaps[randomMapIndex]);
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
        SceneManager.LoadSceneAsync(0); // Replace 1 with the index of your login scene
    }
}
