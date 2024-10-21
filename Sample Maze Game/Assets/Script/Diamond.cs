using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Diamond : MonoBehaviour
{
    public Exit exitScript;
    public AudioClip collectSound;
    private AudioSource audioSource;
    public TextMeshProUGUI diamondCounterText;
    private int diamondCollected = 0;
    public int totalObjectsToCollect = 4;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        updateDiamondCollection();
    }

    public void diamondCollection()
    {
        PlayCollectSound();
        diamondCollected++;
        updateDiamondCollection();

        //win condition
        if (diamondCollected >= totalObjectsToCollect)
        {
            Debug.Log("you win");
            exitScript.whenAllDiaCollected();
        }
    }
    private void PlayCollectSound()
    {
        if (audioSource != null && collectSound != null)
        {
            // Play the sound effect
            audioSource.PlayOneShot(collectSound);
        }
    }
    void updateDiamondCollection()
    {
        diamondCounterText.text = diamondCollected + "/" + totalObjectsToCollect;
    }

}