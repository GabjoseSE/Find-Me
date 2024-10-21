using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinWaypoints : MonoBehaviour
{
    public GameObject coins;
    public Transform[] waypoints;
    public int numberOfObjectsToSpawn = 4;
    private List<Transform> availableWaypoints;
    void Start()
    {
        if (waypoints.Length < numberOfObjectsToSpawn)
        {
            Debug.LogError("Not enough waypoints available for spawning!");
            return;
        }
        spawnCoin();
    }

    void spawnCoin()
    {
        availableWaypoints = new List<Transform>(waypoints);
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            // Select a random waypoint
            int randomIndex = Random.Range(0, availableWaypoints.Count);
            Transform spawnPoint = availableWaypoints[randomIndex];

            // Spawn the object at the random waypoint
            Instantiate(coins, spawnPoint.position, spawnPoint.rotation);

            // Remove the used waypoint from the list so it's not reused
            availableWaypoints.RemoveAt(randomIndex);
        }
    }
}
