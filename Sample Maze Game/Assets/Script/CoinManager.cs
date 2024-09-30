using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class CoinManager : MonoBehaviour
{
    public int coinCount = 0; // Total coins collected
    public Text coinText; // Reference to the UI Text to display the coin count
    public string username; // Username of the logged-in player
    private string updateCoinsURL = "http://192.168.0.14/UnityFindME/update_coins.php"; // Change to your actual PHP URL

    private void Start()
    {
        username = PlayerPrefs.GetString("LoggedInUser", null); // Default to null if not set

        UpdateCoinUI();
    }

    // Function to update the coin count
    public void CollectCoin()
    {
        coinCount++;
        UpdateCoinUI();

        // Check if username is set
        if (string.IsNullOrEmpty(username))
        {
            Debug.LogError("Username is not set! Cannot update coins in the database.");
            return; // Exit the method if the username is not set
        }

        StartCoroutine(UpdateCoinsInDatabase(username, coinCount)); // Save the updated coin count to the database
    }

    private void UpdateCoinUI()
    {
        coinText.text = " " + coinCount.ToString();
    }

    // Coroutine to update the coin count in the database
    private IEnumerator UpdateCoinsInDatabase(string username, int coins)
    {
        WWWForm form = new WWWForm();
        
        // Log the username and coin count for debugging
        Debug.Log("Updating coins in the database for user: " + username + " with coin count: " + coins);

        form.AddField("username", username); // Add the player's username
        form.AddField("coins", coins); // Add the updated coin count

        using (UnityWebRequest www = UnityWebRequest.Post(updateCoinsURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error updating coins in database: " + www.error);
            }
            else
            {
                // Log the raw response from the server
                string responseText = www.downloadHandler.text;
                Debug.Log("Server response: " + responseText);

                // Try to parse the JSON response
                try
                {
                    ServerResponse response = JsonUtility.FromJson<ServerResponse>(responseText);

                    if (response.status == "success")
                    {
                        Debug.Log("Coins updated successfully in the database");
                    }
                    else
                    {
                        Debug.LogError("Error updating coins: " + response.message);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("JSON parsing error: " + e.Message);
                }
            }
        }
    }

    // Call this method to set the username
    public void SetUsername(string user)
{
    username = user;
    Debug.Log("Username set to: " + username); // Log the username being set
}

}
