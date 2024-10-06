using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableLetter : MonoBehaviour
{
    public char letter;
    public WordManager wordManager;
    private void OnTrigger(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wordManager.CollectedLetter(letter);
            Destroy(gameObject);
        }
    }
}
