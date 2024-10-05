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
        ShowLoginPage(); // Start with the login page
    }

    // Show the login page
    public void ShowLoginPage()
    {
        HideAllCanvases();
        loginCanvas.SetActive(true);
    }
    public void ShowDifficultyPage()
    {
        HideAllCanvases();
        difficultyCanvas.SetActive(true);
    }


    // Show the signup page
    public void ShowAboutPage()
    {
        HideAllCanvases();
        aboutCanvas.SetActive(true);
    }

    // Show the main menu
    public void ShowMainMenu()
    {
        HideAllCanvases();
        mainMenuCanvas.SetActive(true);
    }

    // Show the store page
    public void ShowStorePage()
    {
        HideAllCanvases();
        storeCanvas.SetActive(true);
    }

    // Show the options page
    public void ShowOptionsPage()
    {
        HideAllCanvases();
        optionsCanvas.SetActive(true);
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
