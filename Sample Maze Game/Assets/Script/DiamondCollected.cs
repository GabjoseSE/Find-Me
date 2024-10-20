using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondCollected : MonoBehaviour
{
    private Diamond diaManager;

    void Start()
    {
        diaManager = FindObjectOfType<Diamond>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            diaManager.diamondCollection();
            Debug.Log("Diamond Collected");
            Destroy(gameObject);
        }
    }
}
