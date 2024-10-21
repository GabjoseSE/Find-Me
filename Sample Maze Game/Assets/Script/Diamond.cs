using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class Diamond : MonoBehaviour
{
    public GameObject rawImage;
    public VideoPlayer youWin;
    public AudioClip collectSound;
    private AudioSource audioSource;
    public TextMeshProUGUI diamondCounterText;
    private int diamondCollected = 0;
    public int totalObjectsToCollect = 4;

    void Start()
    {
        youWin.gameObject.SetActive(false);
        rawImage.SetActive(false);
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
            rawImage.SetActive(true);
            youWin.gameObject.SetActive(true);
            youWin.Play();
            Time.timeScale = 0;
            Debug.Log("u win bruh");
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