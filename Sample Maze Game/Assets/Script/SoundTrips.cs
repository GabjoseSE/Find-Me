using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTripsManager : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints
    public AudioClip[] tripClips; // Array of sound clips
    private AudioSource tripSource;

    private void Start()
    {
        // Ensure AudioSource is attached to the GameObject
        tripSource = GetComponent<AudioSource>();

        // If no AudioSource is attached, add one
        if (tripSource == null)
        {
            tripSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the manager to each waypoint for sound triggering
        foreach (Transform waypoint in waypoints)
        {
            if (waypoint != null)
            {
                WaypointScript waypointScript = waypoint.gameObject.AddComponent<WaypointScript>();
                waypointScript.Initialize(this); // Pass reference of manager to the waypoint
            }
        }
    }

    // Function to play a random audio clip from the array
    public void PlayRandomClip()
    {
        if (tripClips.Length > 0)
        {
            int randomIndex = Random.Range(0, tripClips.Length); // Choose a random index
            tripSource.PlayOneShot(tripClips[randomIndex]); // Play the selected clip
        }
        else
        {
            Debug.LogWarning("No audio clips assigned!");
        }
    }
}

public class WaypointScript : MonoBehaviour
{
    private SoundTripsManager manager;

    public void Initialize(SoundTripsManager manager)
    {
        this.manager = manager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.PlayRandomClip(); // Play a random sound from the manager
            Debug.Log("Random Sound Played");
            Destroy(gameObject); // Destroy the waypoint after collision
        }
    }
}