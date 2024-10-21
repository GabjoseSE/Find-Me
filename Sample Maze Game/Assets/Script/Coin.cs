using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinManager coinManager;

    private void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
    }

    // Trigger event when player collides with the coin
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object is the player
        {
            coinManager.CollectCoin();
            Debug.Log("Coin collected! Total coins: " + coinManager.coinCount);
            Destroy(gameObject);
        }
    }
}
