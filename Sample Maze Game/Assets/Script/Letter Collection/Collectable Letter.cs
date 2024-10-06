using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableLetter : MonoBehaviour
{
    public char letter;
    public WordManager wordManager;
    public void SetLetter(char letter)
    {
        this.letter = letter;
    }
    private void OnTrigger(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<WordManager>().CollectedLetter(letter);
            Destroy(gameObject);
        }
    }
}
