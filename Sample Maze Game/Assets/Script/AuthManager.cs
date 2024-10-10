using System.Collections;
using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.SceneManagement;
using System;
using UnityEngine.Networking;
using TMPro;

public class AuthManager : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public InputField confirmPasswordInput;
    public Text errorMessageText;
    

    

    // URL to your PHP files
    private string signUpURL = "http://192.168.1.248/UnityFindME/add_player.php"; // Adjust according to your setup
    private string loginURL = "http://192.168.1.248/UnityFindME/login_player.php?"; // You'll create this login PHP file
    private string getCoinsURL = "http://192.168.1.248/UnityFindME/get_player_info.php"; 
    private void Start()
    {
        // Check if the player is already logged in
        if (PlayerPrefs.GetInt("isLoggedIn", 0) == 1) // 0 means not logged in
        {
            string username = PlayerPrefs.GetString("LoggedInUser"); // Retrieve the username
            StartCoroutine(GetPlayerInfo(username)); // Fetch player info from the database
        }
    }
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
            ShowErrorMessage("Username and password cannot be empty!");
            return;
        }

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
                    SceneManager.LoadSceneAsync(0); 
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
            Debug.LogError("Login Error: " + www.error);
            ShowErrorMessage("Login failed! Please check your username and password.");
        }
        else
        {
            string responseText = www.downloadHandler.text;
            Debug.Log("Login Response: " + responseText);

            try
            {
                // Parse the JSON response into LoginResponse object
                LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(responseText);

                // Check if login was successful
                if (loginResponse.status == "success")
                {
                    Debug.Log("Login Success: " + loginResponse.message);
                    PlayerPrefs.SetInt("isLoggedIn", 1); // Mark the player as logged in
                    PlayerPrefs.SetString("LoggedInUser", username); // Save the username

                    // Store the player coins
                    int playerCoins = loginResponse.player.coins;
                    Debug.Log("Player Coins: " + playerCoins);

                    // Now update the UI or other gameplay elements
                    OnLoginSuccess(playerCoins);

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
public void OnLoginSuccess(int coins)
{ 
    CoinDisplay coinDisplay = FindObjectOfType<CoinDisplay>();

    if (coinDisplay != null)
    {
        Debug.Log("CoinDisplay found. Updating the UI...");
        coinDisplay.UpdateCoinDisplay(coins);
    }
    else
    {
        Debug.LogError("CoinDisplay not found in the scene.");
    }
    
    StoreManager storeManager = FindObjectOfType<StoreManager>();

    if (storeManager != null)
    {
        Debug.Log("StoreManager found. Updating the UI...");
        storeManager.UpdateCoinDisplay(coins);
    }
    else
    {
        Debug.LogError("StoreManager not found in the scene.");
    }

    // Hide all canvases and show the main menu
    MainSceneManager mainSceneManager = FindObjectOfType<MainSceneManager>();
    if (mainSceneManager != null)
    {
        mainSceneManager.OnSuccessfulLogin();
        StartCoroutine(GetPlayerInfo(usernameInput.text)); // Start fetching player info
    }
    else
    {
        Debug.LogError("MainSceneManager not found in the scene.");
    }
}


private IEnumerator GetPlayerInfo(string username)
{
    while (true) 
    {
        using (UnityWebRequest www = UnityWebRequest.Get(getCoinsURL + "?username=" + username))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error fetching player info: " + www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Player Info Response: " + responseText);

                try
                {
                    LoginResponse playerInfoResponse = JsonUtility.FromJson<LoginResponse>(responseText);

                    if (playerInfoResponse.status == "success")
                    {
                        // Pass coins and power-ups to UI update
                        OnPlayerInfoUpdate(
                            playerInfoResponse.player.coins, 
                            playerInfoResponse.player.powerups.freeze, 
                            playerInfoResponse.player.powerups.speedup, 
                            playerInfoResponse.player.powerups.invisibility, 
                            playerInfoResponse.player.powerups.navigation
                        );
                    }
                    else
                    {
                        Debug.LogError("Error fetching player info: " + playerInfoResponse.message);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("JSON Parsing Error: " + e.Message);
                }
            }
        }

        yield return new WaitForSeconds(5f); // Fetch every 5 seconds
    }
}

private void OnPlayerInfoUpdate(int updatedCoins, int freeze, int speedup, int invisibility, int navigation)
{
    CoinDisplay coinDisplay = FindObjectOfType<CoinDisplay>();  // Find the CoinDisplay in the scene

    if (coinDisplay != null)
    {
        Debug.Log("Updating coin display with updated value: " + updatedCoins);
        coinDisplay.UpdateCoinDisplay(updatedCoins);  
        
        coinDisplay.UpdatePowerupDisplay(freeze, speedup, invisibility, navigation);

    }
    else
    {
        Debug.LogError("CoinDisplay not found in the scene.");
    }

    StoreManager storeManager = FindObjectOfType<StoreManager>();
    if (storeManager != null)
    {
        Debug.Log("Updating StoreManager with new coin count: " + updatedCoins);
        storeManager.UpdateCoinDisplay(updatedCoins);  // Update the StoreManager with the coin count
    }
    else
    {
        Debug.LogError("StoreManager not found in the scene.");
    }
}




    private void ShowErrorMessage(string message)
    {
        errorMessageText.text = message; // Display the error message
    }
}


[Serializable]
public class Player
{
    public int player_id;  // Matches "player_id" in JSON
    public string username;  // Matches "username" in JSON
    public int coins;  // Matches "coins" in JSON

    // Matches the nested "powerups" object in JSON
    public Powerups powerups;  

    public string created_at;  // Matches "created_at" in JSON
}

[Serializable]
public class LoginResponse
{
    public string status;
    public string message;
    public Player player;
}

[Serializable]
public class Powerups
{
    public int freeze;  // Matches "freeze" in JSON
    public int speedup;  // Matches "speedup" in JSON
    public int invisibility;  // Matches "invisibility" in JSON
    public int navigation;  // Matches "navigation" in JSON
}