using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool isCollected = false;
    public int coinValue = 1;  // Amount of coins collected when this coin is picked up

    private void OnTriggerEnter(Collider other)
    {
        if (!isCollected && other.CompareTag("Player"))  // Assuming your player has the tag "Player"
        {
            isCollected = true;
            Debug.Log("Coin collected!");
            GameManager.instance.AddCoins(coinValue);  // Add coins to the player's total
            Destroy(gameObject);  // Remove the coin from the scene
        }
    }
}


