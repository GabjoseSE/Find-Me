using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public GameObject exitObject;
    void Start()
    {
        exitObject.SetActive(false);
    }

    public void whenAllDiaCollected()
    {
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
