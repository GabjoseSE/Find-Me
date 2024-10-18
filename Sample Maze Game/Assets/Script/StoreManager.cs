using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class StoreManager : MonoBehaviour
{
    public int coinCount;
    public string username; 
    private string updateCoinsURL = "http://192.168.1.248/UnityFindME/update_coins_store.php"; 
    private string buyPowerUpURL = "http://192.168.1.248/UnityFindME/buy_powerup.php"; 

    // Prices for each power-up
    private int freezePrice = 20;
    private int speedUpPrice = 25;
    private int invisibilityPrice = 30;
    private int navigationPrice = 35;

    private void Start()
    {
        username = PlayerPrefs.GetString("LoggedInUser", null); 
        if (!string.IsNullOrEmpty(username))
        {
            StartCoroutine(LoadCoinsFromDatabase(username)); 
        }
        else
        {
            Debug.LogError("Username is not set! Cannot load coins.");
        }
    }

    public void UpdateCoinDisplay(int coins)
    {
        coinCount = coins; 
        Debug.Log("Coins in StoreManager: " + coinCount);
    }

    private IEnumerator LoadCoinsFromDatabase(string username)
    {
    WWWForm form = new WWWForm();
    form.AddField("username", username); 

    using (UnityWebRequest www = UnityWebRequest.Post("http://192.168.1.248/UnityFindME/get_coins.php", form)) 
    {
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error loading coins from database: " + www.error);
        }
        else
        {
            string responseText = www.downloadHandler.text;
            Debug.Log("Coins load response: " + responseText);

            try
            {
                CoinsResponse response = JsonUtility.FromJson<CoinsResponse>(responseText); 

                if (response.status == "success")
                {
                    coinCount = response.coins;
                    Debug.Log("Coins loaded: " + coinCount);
                    UpdateCoinDisplay(coinCount);
                }
                else
                {
                    Debug.LogError("Error loading coins: " + response.message);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("JSON parsing error: " + e.Message);
            }
        }
    }
}

    // Method for buying the freeze power-up
    public void BuyFreezePowerUp()
    {
        StartCoroutine(BuyPowerUp("freeze", freezePrice));
    }

    // Method for buying the speed-up power-up
    public void BuySpeedUpPowerUp()
    {
        StartCoroutine(BuyPowerUp("speedup", speedUpPrice));
    }

    // Method for buying the invisibility power-up
    public void BuyInvisibilityPowerUp()
    {
        StartCoroutine(BuyPowerUp("invisibility", invisibilityPrice));
    }

    // Method for buying the navigation power-up
    public void BuyNavigationPowerUp()
    {
        StartCoroutine(BuyPowerUp("navigation", navigationPrice));
    }

    // Coroutine to handle buying power-ups
    private IEnumerator BuyPowerUp(string powerup, int price)
{
    if (coinCount >= price)
    {
        // Deduct coins
        coinCount -= price;

        // Create a form to send the buy request
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("powerup", powerup);
        form.AddField("price", price);

        using (UnityWebRequest www = UnityWebRequest.Post(buyPowerUpURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error buying power-up: " + www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Buy power-up response: " + responseText);

                try
                {
                    ServerResponse response = JsonUtility.FromJson<ServerResponse>(responseText);
                    if (response.status == "success")
                    {
                        // Update the coins in the database after the purchase
                        StartCoroutine(UpdateCoinsInDatabase(username, coinCount));
                        Debug.Log(powerup + " power-up purchased successfully!");
                        
                        
                       
                    }
                    else
                    {
                        Debug.LogError("Error purchasing power-up: " + response.message);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("JSON parsing error: " + e.Message);
                }
            }
        }
    }
    else
    {
        Debug.LogError("Not enough coins to buy " + powerup + " power-up!");
    }
}


    // Coroutine to update the coin count in the database
    private IEnumerator UpdateCoinsInDatabase(string username, int coins)
    {
        WWWForm form = new WWWForm();
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
                string responseText = www.downloadHandler.text;
                Debug.Log("Server response: " + responseText);
            }
        }
    }
}


