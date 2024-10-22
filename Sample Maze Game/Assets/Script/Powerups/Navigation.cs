using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Navigation : MonoBehaviour
{
    public GameObject player;            // Reference to the player GameObject
    public GameObject exitObject;            // The exitObject GameObject you want to navigate to
    public LineRenderer lineRenderer;    // LineRenderer component to draw the line
    public Button navButton;        // The UI Button that triggers navigation
    public float moveSpeed = 5f;         // Player movement speed

    private bool isNavigating = false;   // To check if navigation is active

    void Start()
    {
        // Add the button listener
        navButton.onClick.AddListener(StartNavigation);

        // Make sure LineRenderer has 2 positions to start with
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (isNavigating)
        {
            // Draw the line from player to exitObject
            lineRenderer.SetPosition(0, player.transform.position);
            lineRenderer.SetPosition(1, exitObject.transform.position);

            // Move the player toward the exitObject if navigation is active
            player.transform.position = Vector3.MoveTowards(player.transform.position, exitObject.transform.position, moveSpeed * Time.deltaTime);

            // Stop navigation if player reaches the exitObject
            if (Vector3.Distance(player.transform.position, exitObject.transform.position) < 0.1f)
            {
                isNavigating = false;
                lineRenderer.enabled = false;  // Hide the line after reaching the exitObject
            }
        }
    }

    void StartNavigation()
    {
        isNavigating = true;
        lineRenderer.enabled = true;  // Enable the line renderer
    }
}
