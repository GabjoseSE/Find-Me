using UnityEngine;
using UnityEngine.UI;

public class LetterWall : MonoBehaviour
{
    public char assignedLetter; // The letter assigned to this wall
    public bool isCollected = false; // Whether the letter has been collected
    public Button collectButton; // The collect button

    private WordManager wordManager; // Reference to the WordManager script

    private void Start()
    {
        wordManager = WordManager.instance;
        collectButton.onClick.AddListener(CollectLetter);
        collectButton.gameObject.SetActive(false);
    }

    // Show the collect button when the player is facing the LetterWall
private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.CompareTag("Player")) // Check if the collider is the player
    {
        Vector3 playerForward = other.transform.forward;
        Vector3 directionToLetter = (transform.position - other.transform.position).normalized;
        float dotProduct = Vector3.Dot(playerForward, directionToLetter);
        if (dotProduct > 0)
        {
            collectButton.gameObject.SetActive(true);
            Debug.Log("Assigned letter: " + assignedLetter);
        }
    }
}

    // Hide the collect button when the player is no longer facing the LetterWall
private void OnTriggerExit(Collider other)
{
    if (other.gameObject.CompareTag("Player")) // Check if the collider is the player
    {
        collectButton.gameObject.SetActive(false);
    }
}

    // Collect the letter and update the WordManager
    // Collect the letter and update the WordManager
// Collect the letter and update the WordManager
public void CollectLetter()
{
   if (!isCollected)
    {
        isCollected = true;
        WordManager.instance.CollectLetter(assignedLetter); // Pass the assignedLetter variable to the WordManager script
        collectButton.gameObject.SetActive(false); // Hide the collect button after collection
    }
}
}