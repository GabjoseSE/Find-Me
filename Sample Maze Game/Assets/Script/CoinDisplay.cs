using TMPro;
using UnityEngine;

public class CoinDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] coinTexts;  // Array to hold multiple TMP text fields

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
}
