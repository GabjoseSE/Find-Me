using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image[] hearts; // Array to store heart images
    public int maxHealth = 5; // Maximum player health
    public int currentHealth = 2; // Current player's health (start with 3 hearts)

    void Start()
    {
        UpdateHearts(); // Update the heart display based on the current health
    }

    // Function to update hearts display
    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = (i < currentHealth); // Show or hide heart
        }
    }

    // Call this function when the player loses a heart
    // Call this function when the player loses a heart
public void LoseHeart()
{
    if (currentHealth > 0)
    {
        currentHealth--; // Decrease health
        UpdateHearts();  // Update the UI
    }
}


    // Call this function when the player collects a heart
    public void GainHeart()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++; // Increase health
            UpdateHearts();  // Update the UI
        }
    }

    // Call this when the player takes damage
    // Call this when the player takes damage
public void TakeDamage(int damage)
{
    for (int i = 0; i < damage; i++)
    {
        LoseHeart(); // Call LoseHeart for each point of damage
    }

    Debug.Log($"Player takes damage. Current health: {currentHealth}");

    // Check for death
    if (currentHealth <= 0)
    {
        Die();
    }
}




    void Die()
    {
        // Handle player death
        Debug.Log("Player is dead!");
        // Implement what happens when the player dies (restart level, show game over, etc.)
    }
}
