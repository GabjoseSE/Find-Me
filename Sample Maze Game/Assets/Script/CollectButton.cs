using UnityEngine;
using UnityEngine.UI;

public class CollectButton : MonoBehaviour
{
    public LetterWall letterWall; // The LetterWall script

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(letterWall.CollectLetter);
    }
}