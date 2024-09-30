using UnityEngine;
using System.Collections.Generic;

public class WordManager : MonoBehaviour
{
    public List<string> wordList = new List<string> { "MAZE", "GAME", "WORD", "CODE" }; // Add more words
    public string selectedWord;
    public Texture[] letterTextures; // Array for letter textures
    public string wallTag = "LetterWall"; // Tag for walls

    private void Start()
    {
        SelectRandomWord();
        SpawnLetters();
    }

    // Selects a random word from the list
    // In WordManager.cs
private void SelectRandomWord()
{
    selectedWord = wordList[Random.Range(0, wordList.Count)];
    
    // Set the target word in PlayerInventory
    PlayerInventory playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    if (playerInventory != null)
    {
        playerInventory.targetWord = selectedWord; // Set the target word for the player
    }
}


    // Spawns letters on random tagged walls
   private void SpawnLetters()
{
    char[] letters = selectedWord.ToCharArray();
    
    GameObject[] walls = GameObject.FindGameObjectsWithTag(wallTag);
    List<GameObject> usedWalls = new List<GameObject>();

    for (int i = 0; i < letters.Length; i++)
    {
        if (usedWalls.Count >= walls.Length)
        {
            Debug.LogWarning("Not enough walls to place all letters!");
            return;
        }

        GameObject randomWall;
        do
        {
            randomWall = walls[Random.Range(0, walls.Length)];
        } while (usedWalls.Contains(randomWall));

        usedWalls.Add(randomWall);

        // Calculate random position and change wall material as before
        Vector3 randomPosition = GetRandomPositionOnWall(randomWall);

        // Change the material of the wall to display the letter texture
        Renderer wallRenderer = randomWall.GetComponent<Renderer>();
        if (wallRenderer != null)
        {
            Material wallMaterial = wallRenderer.material;

            int letterIndex = GetLetterIndex(letters[i]);
            if (letterIndex >= 0 && letterIndex < letterTextures.Length)
            {
                wallMaterial.mainTexture = letterTextures[letterIndex];
            }

            // Get the LetterWall component and assign the letter
            LetterWall letterWall = randomWall.GetComponent<LetterWall>();
            if (letterWall != null)
            {
                letterWall.SetAssignedLetter(letters[i]); // Assign letter to the wall
            }
        }
    }
}



    // Gets a random position on the surface of a wall using its collider bounds
    private Vector3 GetRandomPositionOnWall(GameObject wall)
    {
        Collider wallCollider = wall.GetComponent<Collider>();
        if (wallCollider != null)
        {
            Vector3 minBounds = wallCollider.bounds.min;
            Vector3 maxBounds = wallCollider.bounds.max;

            // Randomize position within the bounds of the wall
            float randomX = Random.Range(minBounds.x, maxBounds.x);
            float randomZ = Random.Range(minBounds.z, maxBounds.z);

            return new Vector3(randomX, (minBounds.y + maxBounds.y) / 2, randomZ); // Middle height of the wall
        }
        return wall.transform.position; // Default to the wall's position if no collider is found
    }

    // Get the index of the letter texture based on the letter character
    private int GetLetterIndex(char letter)
    {
        // Assuming your textures are in order A-Z
        return letter - 'A'; // Convert letter to index (A=0, B=1, ..., Z=25)
    }
}
