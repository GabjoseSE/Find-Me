using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Diamond : MonoBehaviour
{
    public TextMeshProUGUI diamondCounterText;
    private int diamondCollected = 0;
    public int totalObjectsToCollect = 4;

    void Start()
    {

        updateDiamondCollection();
    }

    public void diamondCollection()
    {
        diamondCollected++;
        updateDiamondCollection();

        //win condition
        if (diamondCollected >= totalObjectsToCollect)
        {
            Debug.Log("u win bruh");
        }
    }
    void updateDiamondCollection()
    {
        diamondCounterText.text = diamondCollected + "/" + totalObjectsToCollect;
    }
}