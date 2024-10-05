using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public string[] wordList = {"APPLE", "BRICK", "CRANE", "DRIVE", "EAGLE", "FLOCK", "GRAPE", "HOTEL", "JELLY", "KNOCK",
    "LEMON", "MELON", "NOVEL", "PEARL", "QUILT", "ROBIN", "STORM", "TABLE", "VIVID", "WHALE",
    "XENON", "YACHT", "ZEBRA"};
    private string selectedWords;
    private List<char> collectedLetters = new List<char>();
    public TMPro.TextMeshProUGUI wordDisplay;
    void Start()
    {
        selectedWords = wordList[Random.Range(0, wordList.Length)];
        wordDisplay.text = $"Collect the letters to form: {selectedWords}";
    }
    public void CollectedLetter(char letter)
    {
        collectedLetters.Add(letter);
        CheckIfWordIsComplete();
    }
    private void CheckIfWordIsComplete()
    {
        string formedWord = new string(collectedLetters.ToArray());
        if (formedWord.Equals(selectedWords))
        {
            Debug.Log("You win for now...");
        }
    }
}
