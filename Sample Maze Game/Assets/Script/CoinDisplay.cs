using TMPro;
using UnityEngine;

public class CoinDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] coinTexts;  // Array to hold multiple TMP text fields
    public TextMeshProUGUI[] freezeText;   // TMP text for freeze power-up
    public TextMeshProUGUI[] speedupText;  // TMP text for speedup power-up
    public TextMeshProUGUI[] invisibilityText; // TMP text for invisibility power-up
    public TextMeshProUGUI[] navigationText;   // TMP text for navigation power-up

    public void UpdateCoinDisplay(int coins)
    {
        Debug.Log("Updating coin display with value: " + coins); // Check if this is being called
        if (coinTexts != null && coinTexts.Length > 0)
        {
            // Loop through each TextMeshProUGUI element and update its text
            foreach (var coinText in coinTexts)
            {
                if (coinText != null) // Check if the current text field is assigned
                {
                    coinText.text = coins.ToString();  // Update the text to display the coin count
                }
                else
                {
                    Debug.LogError("One of the coinText elements is not assigned in CoinDisplay.");
                }
            }
        }
        else
        {
            Debug.LogError("coinTexts array is not assigned or empty in CoinDisplay.");
        }
    }
    public void UpdatePowerupDisplay(int freeze, int speedup, int invisibility, int navigation)
    {
        if (freezeText != null && freezeText.Length > 0)
            foreach (var freezeText in freezeText)
            {
                if (freezeText != null) // Check if the current text field is assigned
                {
                    freezeText.text = freeze.ToString();  // Update the text to display the coin count
                }
                else
                {
                    Debug.LogError("One of the freezeText elements is not assigned.");
                }
            }

        if (speedupText != null && speedupText.Length > 0)
            foreach (var speedupText in speedupText)
            {
                if (speedupText != null) // Check if the current text field is assigned
                {
                    speedupText.text = speedup.ToString();  // Update the text to display the coin count
                }
                else
                {
                    Debug.LogError("One of the speedupText elements is not assigned.");
                }
            }

        if (invisibilityText != null && invisibilityText.Length > 0)
            foreach (var invisibilityText in invisibilityText)
            {
                if (invisibilityText != null) // Check if the current text field is assigned
                {
                    invisibilityText.text = invisibility.ToString();  // Update the text to display the coin count
                }
                else
                {
                    Debug.LogError("One of the invisibilityText elements is not assigned.");
                }
            }
        if (navigationText != null && navigationText.Length > 0)
            foreach (var navigationText in navigationText)
            {
                if (navigationText != null) // Check if the current text field is assigned
                {
                    navigationText.text = navigation.ToString();  // Update the text to display the coin count
                }
                else
                {
                    Debug.LogError("One of the navigationText elements is not assigned.");
                }
            }
    }
}