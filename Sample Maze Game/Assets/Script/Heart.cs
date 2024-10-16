using UnityEngine;

public class Heart : MonoBehaviour
{
    // Reference to the PlayerHealth component
    private PlayerHealth playerHealth;

    private void Start()
    {
        // Find the PlayerHealth in the scene
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Trigger event when player collides with the heart
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            playerHealth.GainHeart();
            Debug.Log("Heart collected! Current health: " + playerHealth.currentHealth);
            Destroy(gameObject);
        }
    }
}
