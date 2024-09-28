using UnityEngine;

public class Coin : MonoBehaviour
{
    // Reference to the CoinManager
    private CoinManager coinManager;

    private void Start()
    {
        // Find the CoinManager in the scene
        coinManager = FindObjectOfType<CoinManager>();
    }

    // Trigger event when player collides with the coin
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object is the player
        {
            // Call the CoinManager to increment the coin count
            coinManager.CollectCoin();
            Debug.Log("Coin collected! Total coins: " + coinManager.coinCount);

            // Destroy the coin object after collection
            Destroy(gameObject);
        }
    }
}
