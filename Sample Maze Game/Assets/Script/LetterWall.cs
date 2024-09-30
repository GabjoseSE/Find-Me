using UnityEngine;
using UnityEngine.UI;

public class LetterWall : MonoBehaviour
{
    public char assignedLetter; // Assigned letter for this wall
    public GameObject collectButton; // UI Button
    public float collectRadius = 3f; // Radius for detection
    private Transform player; // Reference to the player Transform
    public Text buttonText; // Reference to the Text component on the button

    private bool isButtonShown = false; // Track if button is shown
    private PlayerInventory playerInventory; // Declare playerInventory variable here

    private void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player object not found. Make sure the player is tagged as 'Player'.");
            return;
        }

        // Get the PlayerInventory component from the player
        playerInventory = player.GetComponent<PlayerInventory>();

        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory component not found on the player.");
            return;
        }

        if (collectButton == null)
        {
            Debug.LogError("Collect button not assigned.");
        }
        else
        {
            collectButton.SetActive(false);
        }
    }

    private void Update()
    {
        if (player == null || playerInventory == null)
        {
            return;
        }

        // Check if the player is within the collect radius
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= collectRadius)
        {
            // Player is in range, show collect button
            if (!isButtonShown && collectButton != null)
            {
                collectButton.SetActive(true);
                buttonText.text = $"Collect {assignedLetter}";
                Debug.Log($"Collect button shown. Letter to collect: {assignedLetter}");
                isButtonShown = true;
            }
        }
        else
        {
            // Player is out of range, hide collect button
            if (isButtonShown && collectButton != null)
            {
                collectButton.SetActive(false);
                Debug.Log("Collect button hidden.");
                isButtonShown = false;
            }
        }
    }

    // This method will be called when the button is clicked
    public void OnCollectButtonClick()
    {
        Debug.Log("Collect button clicked.");
        CollectLetter();
    }

    // Collect the letter
    public void CollectLetter()
    {
        Debug.Log($"Attempting to collect letter: {assignedLetter}"); // Log the letter being collected

        // Add the collected letter to the player's inventory
        if (playerInventory != null)
        {
            Debug.Log("Adding letter to player inventory."); // Log that the letter is being added
            playerInventory.AddLetter(assignedLetter); // Call the method to add the letter
            Debug.Log($"Letter {assignedLetter} collected successfully."); // Confirm successful collection
        }
        else
        {
            Debug.LogError("PlayerInventory is null! Cannot collect letter."); // Log error if PlayerInventory is null
        }

        // Hide the collect button after collecting the letter
        if (collectButton != null)
        {
            collectButton.SetActive(false);
            Debug.Log("Collect button hidden after collection."); // Log that the button is hidden
        }

        isButtonShown = false;

        // Destroy the letter wall after collecting the letter
        // Destroy(gameObject); // You may want to destroy or deactivate the wall
        Debug.Log("Letter wall destroyed after collection."); // Log that the wall is destroyed
    }




    // Method to assign a letter to the wall
    public void SetAssignedLetter(char letter)
    {
        assignedLetter = letter;
        Debug.Log($"Assigned letter set to: {assignedLetter}"); // Log the assigned letter
    }
}
