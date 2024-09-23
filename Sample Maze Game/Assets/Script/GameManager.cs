using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text coinsUIText;   // Reference to the UI Text component showing the player's coin count
    private int totalCoins = 0;

    private void Awake()
    {
        // Make sure there's only one instance of GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Make sure GameManager persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to add coins
    public void AddCoins(int value)
    {
        totalCoins += value;
        Debug.Log("Coins Added: " + value + ", Total Coins: " + totalCoins);
        UpdateCoinUI();
        SaveCoinsToDatabase();  // Optional: Save coins to SQLite
    }

    // Update the coin UI text
    private void UpdateCoinUI()
    {
        coinsUIText.text = "Coins: " + totalCoins.ToString();
    }

    // Save the coin count to the database
    private void SaveCoinsToDatabase()
{
    string dbPath = "URI=file:" + Application.persistentDataPath + "/findme.db"; // Path to your SQLite database
    using (var connection = new SqliteConnection(dbPath))
    {
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            // Update the total coins for the current player in the database.
            // Assuming you have a variable `currentUsername` storing the logged-in user's username.
            command.CommandText = "UPDATE players SET coins = @coins WHERE username = @username";
            command.Parameters.AddWithValue("@coins", totalCoins);
            command.Parameters.AddWithValue("@username", AuthManager.currentUsername); // Replace with actual username from your login system

            command.ExecuteNonQuery();
            Debug.Log("Coin added to database");
        }
    }
}
    


}

