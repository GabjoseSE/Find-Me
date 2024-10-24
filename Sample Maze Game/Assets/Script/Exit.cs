using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public Button navButton;
    public GameObject exitObject;
    void Start()
    {
        navButton.interactable = false;
        exitObject.SetActive(false);
    }

    public void whenAllDiaCollected()
    {
        navButton.interactable = true;
        exitObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && exitObject.activeInHierarchy)
        {
            SceneManager.LoadSceneAsync(20);
            Debug.Log("u win bruh");
        }
    }
}
