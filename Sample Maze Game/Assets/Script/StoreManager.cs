using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class StoreManager : MonoBehaviour
{
    public Text coinsText; // To display current coin count
    public GameObject[] powerUpPanels; // Array to hold panels for each power-up
    public Button[] confirmButtons; // Array of confirm buttons for each power-up
    public Button[] cancelButtons; // Array of cancel buttons for each power-up

    private int selectedPowerUpID; // ID of the power-up the player is trying to buy
    private int selectedPowerUpCost; // Cost of the selected power-up
    private int coins; // Player's coins
    public string username; // Player's username

    // Example power-up costs
    private int[] powerUpCosts = { 10, 15, 20, 25 };

    void Start()
    {
        // Fetch coins from the database
        StartCoroutine(GetCoinsFromDatabase(username));

    }

    // Function to fetch coins from the database
    public IEnumerator GetCoinsFromDatabase(string username)
    {
        // Your existing logic to get coins
        // Example code:
       string url = "http://192.168.1.248/UnityFindME/get_coins.php?username=" + UnityWebRequest.EscapeURL(username);


        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Get Coins Error: " + www.error);
            }
            else
            {
                // Assuming the response is in a specific format
                string responseText = www.downloadHandler.text;
                // Parse responseText to get coins
                Debug.Log("Coins fetched: " + responseText);
                // Update player's coins variable or UI
            }
        }
    }


    // Function to open the specific power-up panel
    public void OnPowerUpButtonClick(int powerUpID)
    {
        selectedPowerUpID = powerUpID;
        selectedPowerUpCost = powerUpCosts[powerUpID];

        // Hide all panels before showing the selected one
        for (int i = 0; i < powerUpPanels.Length; i++)
        {
            powerUpPanels[i].SetActive(false);
        }

        // Show the selected power-up panel
        powerUpPanels[powerUpID].SetActive(true);

        // Set up the confirm and cancel buttons for the selected power-up
        confirmButtons[powerUpID].onClick.RemoveAllListeners();
        confirmButtons[powerUpID].onClick.AddListener(ConfirmPurchase);
        cancelButtons[powerUpID].onClick.RemoveAllListeners();
        cancelButtons[powerUpID].onClick.AddListener(CancelPurchase);
    }

    // Function to confirm the purchase
    private void ConfirmPurchase()
    {
        Debug.Log("Current coins before purchase: " + coins);
        Debug.Log("Selected Power-Up Cost: " + selectedPowerUpCost);

        if (coins >= selectedPowerUpCost)
        {
            // Deduct coins and proceed with purchase
            coins -= selectedPowerUpCost;
            coinsText.text = "Coins: " + coins; // Update UI text

            // Call the function to update the database here
            StartCoroutine(PurchasePowerUp(selectedPowerUpID, selectedPowerUpCost));

            Debug.Log("Purchased Power-Up " + (selectedPowerUpID + 1));
        }
        else
        {
            Debug.Log("Not enough coins!");
        }

        // Hide the current panel
        powerUpPanels[selectedPowerUpID].SetActive(false);
    }

    // Function to cancel the purchase
    private void CancelPurchase()
    {
        // Hide the current power-up panel
        powerUpPanels[selectedPowerUpID].SetActive(false);
    }

    // Coroutine to update the database after the purchase
    private IEnumerator PurchasePowerUp(int powerUpID, int powerUpCost)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("coins", coins);
        form.AddField("powerup_id", powerUpID + 1); // Assuming powerUpID starts from 0, add 1 for DB

        using (UnityWebRequest www = UnityWebRequest.Post("http://192.168.1.248/UnityFindME/purchase_powerup.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error purchasing power-up: " + www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Server response: " + responseText);

                // Handle the response (update coins and inventory)
                ServerResponse response = JsonUtility.FromJson<ServerResponse>(responseText);

                if (response.status == "success")
                {
                    Debug.Log("Power-up purchased successfully and coins deducted.");
                }
                else
                {
                    Debug.LogError("Purchase failed: " + response.message);
                }
            }
        }
    }
    private void UpdateCoinsUI()
    {
        coinsText.text = "Coins: " + coins; // Update UI text
    }

    // Call this method whenever you want to refresh the coin display
    public void RefreshCoinDisplay()
    {
        StartCoroutine(GetCoinsFromDatabase(username));
    }
}
