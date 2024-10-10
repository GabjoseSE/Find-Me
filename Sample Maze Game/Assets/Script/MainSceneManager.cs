using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public GameObject loginCanvas;
    public GameObject aboutCanvas;
    public GameObject mainMenuCanvas;
    public GameObject storeCanvas;
    public GameObject optionsCanvas;

    public GameObject difficultyCanvas;


    private void Start()
    {
        // Check if the user is already logged in (from previous scenes)
        if (PlayerPrefs.GetInt("isLoggedIn", 0) == 1)
        {
            ShowMainMenu(); // Bypass login page
        }
        else
        {
            ShowLoginPage(); // Show login page as usual
        }
    }

    // Show the login page
    public void ShowLoginPage()
    {
        HideAllCanvases();
        loginCanvas.SetActive(true);
        Debug.Log("Showloginpage");

    }
    public void ShowDifficultyPage()
    {
        HideAllCanvases();
        difficultyCanvas.SetActive(true);
        Debug.Log("ShowDifficultypage");
    }


    // Show the signup page
    public void ShowAboutPage()
    {
        HideAllCanvases();
        aboutCanvas.SetActive(true);
        Debug.Log("ShowAboutpage");
    }

    // Show the main menu
    public void ShowMainMenu()
    {
        HideAllCanvases();
        mainMenuCanvas.SetActive(true);
        Debug.Log("ShowMainMenu");
    }

    // Show the store page
    public void ShowStorePage()
    {
        HideAllCanvases();
        storeCanvas.SetActive(true);
        Debug.Log("ShowStorepage");
    }

    // Show the options page
    public void ShowOptionsPage()
    {
        HideAllCanvases();
        optionsCanvas.SetActive(true);
        Debug.Log("ShowOptionspage");
    }

    // Hide all canvases
    private void HideAllCanvases()
    {
        loginCanvas.SetActive(false);
        aboutCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
        storeCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        difficultyCanvas.SetActive(false);
    }

    // Call this method to transition to the main menu after a successful login
    public void OnSuccessfulLogin()
{
    HideAllCanvases();  // Hide all canvases
    ShowMainMenu();     // Optionally show the main menu
}
public void OnSuccessfulSignup()
{
    HideAllCanvases();  // Hide all canvases
    ShowLoginPage();     // Optionally show the main menu
}
}
