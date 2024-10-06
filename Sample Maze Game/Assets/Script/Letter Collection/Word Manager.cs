using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordManager : MonoBehaviour
{
    public string[] wordList = {"APPLE", "BRICK", "CRANE", "DRIVE", "EAGLE", "FLOCK", "GRAPE", "HOTEL", "JELLY", "KNOCK",
    "LEMON", "MELON", "NOVEL", "PEARL", "QUILT", "ROBIN", "STORM", "TABLE", "VIVID", "WHALE",
    "XENON", "YACHT", "ZEBRA"};
    private string selectedWords;
    private List<char> collectedLetters = new List<char>();
    public Transform letterUIPanel;
    public GameObject letterUIImagePrefab;
    public Dictionary<char, Sprite> letterSprites = new Dictionary<char, Sprite>();
    public Sprite[] sprites;
    public char[] letters;
    public GameObject[] letterPrefabs;
    public Transform[] collectibleSpawnPoints;
    void Start()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            letterSprites[letters[i]] = sprites[i];
        }
        selectedWords = wordList[Random.Range(0, wordList.Length)];
        Debug.Log("Selected Word: " + selectedWords);
        SpawnCollectibleLetters();

    }
    public void CollectedLetter(char letter)
    {
        if (selectedWords.Contains(letter.ToString()))
        {
            collectedLetters.Add(letter);
            UpdateCollectedLettersUI(letter);
            CheckIfWordIsComplete();
        }
        else
        {
            Debug.Log("Collected letter is not part of the selected word.");
        }
    }
    private void UpdateCollectedLettersUI(char letter)
    {
        GameObject newLetterImage = Instantiate(letterUIImagePrefab, letterUIPanel);
        Image letterImage = newLetterImage.GetComponent<Image>();
        Sprite collectedLetterSprite = GetLetterSprite(letter);
        letterImage.sprite = collectedLetterSprite;
    }
    private Sprite GetLetterSprite(char letter)
    {
        if (letterSprites.ContainsKey(letter))
        {
            return letterSprites[letter];
        }
        else
        {
            Debug.LogError("No sprite found for letter: " + letter);
            return null;
        }
    }
    private void CheckIfWordIsComplete()
    {
        string formedWord = new string(collectedLetters.ToArray());
        if (formedWord.Equals(selectedWords))
        {
            Debug.Log("You're done for now...");
        }
    }
    private void SpawnCollectibleLetters()
    {
        HashSet<char> uniqueLetters = new HashSet<char>(selectedWords.ToCharArray());
        List<Transform> availableSpawnPoints = new List<Transform>(collectibleSpawnPoints);

        // Shuffle the letters to randomize their distribution
        List<char> shuffledLetters = new List<char>(uniqueLetters);
        for (int i = 0; i < shuffledLetters.Count; i++)
        {
            char temp = shuffledLetters[i];
            int randomIndex = Random.Range(i, shuffledLetters.Count);
            shuffledLetters[i] = shuffledLetters[randomIndex];
            shuffledLetters[randomIndex] = temp;
        }
        foreach (char letter in shuffledLetters)
        {
            if (availableSpawnPoints.Count > 0)
            {
                int index = System.Array.IndexOf(letters, letter);
                if (index >= 0 && index < letterPrefabs.Length)
                {
                    // Get a random spawn point
                    Transform spawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
                    GameObject collectible = Instantiate(letterPrefabs[index], spawnPoint.position, Quaternion.identity);
                    collectible.GetComponent<CollectableLetter>().SetLetter(letter);

                    // Remove the spawn point to avoid reuse
                    availableSpawnPoints.Remove(spawnPoint);
                }
                else
                {
                    Debug.LogError("No prefab found for letter: " + letter);
                }
            }
        }
    }
}
