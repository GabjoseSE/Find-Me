using System.Collections;
using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.SceneManagement;
using System;
using UnityEngine.Networking;

public class AuthManager : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public InputField confirmPasswordInput;
     public Text errorMessageText;

    // URL to your PHP files
    private string signUpURL = "http://192.168.1.248/UnityFindME/add_player.php"; // Adjust according to your setup
    private string loginURL = "http://192.168.1.248/UnityFindME/login_player.php?"; // You'll create this login PHP file

    // Signup Button
    public void SignUp()
    {
        if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text))
    {
        ShowErrorMessage("Username and password cannot be empty!"); // Display error message for empty fields
        return; // Exit the method if fields are empty
    }
        if (passwordInput.text != confirmPasswordInput.text)
    {
        Debug.LogError("Signup Failed! Password and Confirm Password do not match.");
        ShowErrorMessage("Passwords do not match!"); // Display error message
        return; // Exit the method if the passwords don't match
    }
        StartCoroutine(SignUpUser(usernameInput.text, passwordInput.text));
    }

    // Login Button
    public void Login()
    {
        if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text))
    {
        ShowErrorMessage("Username and password cannot be empty!"); // Display error message for empty fields
        return; // Exit the method if fields are empty
    }

    // Proceed with the login if fields are filled
    StartCoroutine(LoginUser(usernameInput.text, passwordInput.text));
    

        
    }

    // Coroutine for signup
    private IEnumerator SignUpUser(string username, string password)
{
    WWWForm form = new WWWForm();
    form.AddField("username", username);
    form.AddField("password", password);

    using (UnityWebRequest www = UnityWebRequest.Post(signUpURL, form))
    {
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Sign Up Error: " + www.error);
            ShowErrorMessage("Sign Up Error: " + www.error); // Display error message
        }
        else
        {
            // Log the raw response
            string responseText = www.downloadHandler.text;
            Debug.Log("Sign Up Success: " + responseText); // Log the response

            // Try parsing the JSON
            try
            {
                LoginResponse signUpResponse = JsonUtility.FromJson<LoginResponse>(responseText);

                if (signUpResponse.status == "success")
                {
                    SceneManager.LoadSceneAsync(0); // Go to login page
                }
                else
                {
                    ShowErrorMessage(signUpResponse.message); // Display error message for username already exists
                }
            }
            catch (Exception e)
            {
                Debug.LogError("JSON Parsing Error: " + e.Message);
            }
        }
    }
}


    // Coroutine for login
    private IEnumerator LoginUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(loginURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                ShowErrorMessage("Login failed! Please check your username and password.");
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Login Response: " + responseText);

                try
                {
                    LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(responseText);

                    // Inside the LoginUser coroutine after a successful login
                    if (loginResponse.status == "success")
{
    // Save the logged-in username for later use
    PlayerPrefs.SetString("LoggedInUser", username); // Save username to PlayerPrefs

    // Get the CoinManager and set the username
    CoinManager coinManager = FindObjectOfType<CoinManager>();
    if (coinManager != null)
    {
        coinManager.SetUsername(username); // Set the username in CoinManager
    }

    Debug.Log("Login Success! User: " + username);

    // Load the next scene (e.g., the game scene)
    SceneManager.LoadSceneAsync(2);
}


                    else
                    {
                        ShowErrorMessage(loginResponse.message);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("JSON Parsing Error: " + e.Message);
                }
            }
        }
    }


    private void ShowErrorMessage(string message)
    {
        errorMessageText.text = message; // Display the error message
    }
}
