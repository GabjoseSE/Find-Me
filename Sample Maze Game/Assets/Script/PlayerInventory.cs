using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public Text collectedLettersText; // Reference to the Text UI component
    private List<char> collectedLetters = new List<char>(); // Store collected letters
    public string targetWord; // The word the player is trying to collect
    public Text targetWordText; // UI Text component to display the target word

    private void Start()
    {
        // Initialize UI elements
        if (collectedLettersText != null)
        {
            collectedLettersText.text = " "; // Initial text
        }

        // Display the target word in the UI
        if (targetWordText != null)
        {
            targetWordText.text = targetWord; // Show the initial target word
        }
    }

    // Method to add a letter to the player's inventory
    public void AddLetter(char letter)
    {
        if (!collectedLetters.Contains(letter)) // Prevent duplicate letters
        {
            collectedLetters.Add(letter);
            UpdateCollectedLettersUI();
            CheckForCompletion(); // Check if the word is complete
        }
        else
        {
            Debug.Log($"Letter {letter} already collected.");
        }
    }


    // Update the collected letters UI
    private void UpdateCollectedLettersUI()
    {
        // Create a string representation of collected letters
        string collectedLettersString = " ";

        foreach (char c in collectedLetters)
        {
            collectedLettersString += c + " "; // Add each collected letter
        }

        collectedLettersText.text = collectedLettersString; // Update the UI text
        Debug.Log("Updated collected letters UI: " + collectedLettersString);
    }
    public void CheckForCompletion()
    {
        // Create a string from the collected letters
        string collectedString = new string(collectedLetters.ToArray());

        // Check if it matches the target word
        if (collectedString == targetWord)
        {
            Debug.Log("Target word collected!");
            // Here you could trigger a win condition or end the game
        }
        else
        {
            Debug.Log("Keep collecting letters!");
        }
    }

}
