using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Needed for Button
using UnityEngine.AI; // Needed for NavMesh

public class NavigationGuide : MonoBehaviour
{
    public Transform player;             // The player object
    public Transform exit;               // The exit object (target)
    public LineRenderer lineRenderer;    // The LineRenderer component
    public float heightOffset = 0.5f;    // Height offset for the line
    public float pointRemoveDistance = 0.5f;  // Distance threshold for removing passed points

    private NavMeshPath path;            // NavMeshPath to calculate pathfinding
    private List<Vector3> pathPoints = new List<Vector3>();  // List to store points along the path
    private bool isPathActive = false;   // To track if the path is active

    public Button activateButton;         // Reference to the UI Button

    void Start()
    {
        path = new NavMeshPath();  // Initialize NavMeshPath
        lineRenderer.useWorldSpace = true;  // Make sure LineRenderer is in world space

        // Add listener to the button
        if (activateButton != null)
        {
            activateButton.onClick.AddListener(ActivatePath);
        }
    }

    void Update()
    {
        // Only update path if it is active
        if (isPathActive)
        {
            // Update path from player to exit
            UpdatePath();

            // Update the line renderer based on the path points
            UpdateLineRenderer();

            // Remove points that the player has passed
            RemovePassedPoints();
        }
    }

    // Activates the path
    public void ActivatePath()
    {
        isPathActive = true; // Set the path as active
        UpdatePath(); // Update the path immediately

        // Disable the button to make it uninteractable
        if (activateButton != null)
        {
            activateButton.interactable = false; // Disable the button
        }
    }

    // Updates the path using Unity's NavMesh
    void UpdatePath()
    {
        if (NavMesh.CalculatePath(player.position, exit.position, NavMesh.AllAreas, path))
        {
            // Clear the old path points
            pathPoints.Clear();

            // Add the new points from the NavMesh path
            foreach (Vector3 point in path.corners)
            {
                // Add a height offset so the line appears above the ground
                Vector3 adjustedPoint = point;
                adjustedPoint.y += heightOffset;
                pathPoints.Add(adjustedPoint);
            }
        }
    }

    // Updates the LineRenderer based on the current path points
    void UpdateLineRenderer()
    {
        lineRenderer.positionCount = pathPoints.Count;
        for (int i = 0; i < pathPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, pathPoints[i]);
        }
    }

    // Removes points from the path if the player has passed them
    void RemovePassedPoints()
    {
        for (int i = pathPoints.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(player.position, pathPoints[i]) < pointRemoveDistance)
            {
                pathPoints.RemoveAt(i);
            }
        }
    }
}