using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondWaypoints : MonoBehaviour
{
    public GameObject diamond;
    public Transform[] waypoints;
    public int numberOfObjectsToSpawn = 4;
    public Diamond diaManager;
    private List<Transform> availableWaypoints;
    void Start()
    {
        if (waypoints.Length < numberOfObjectsToSpawn)
        {
            Debug.LogError("Not enough waypoints available for spawning!");
            return;
        }
        spawnDiamond();
    }

    void spawnDiamond()
    {
        availableWaypoints = new List<Transform>(waypoints);
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            // Select a random waypoint
            int randomIndex = Random.Range(0, availableWaypoints.Count);
            Transform spawnPoint = availableWaypoints[randomIndex];

            // Spawn the object at the random waypoint
            Instantiate(diamond, spawnPoint.position, spawnPoint.rotation);

            // Remove the used waypoint from the list so it's not reused
            availableWaypoints.RemoveAt(randomIndex);
        }
    }
}
