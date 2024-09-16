using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBG : MonoBehaviour
{
    public static MainMenuBG instance;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}