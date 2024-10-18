using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager instance;

    // Power-up values
    public int freeze;
    public int speedup;
    public int invisibility;
    public int navigation;

    private void Awake()
    {
        // Ensure that the object persists across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadPowerups(); 
        StartCoroutine(LoadPowerupsEverySecond());
    }

    public void LoadPowerups()
    {
        freeze = PlayerPrefs.GetInt("Freeze", 0); 
        speedup = PlayerPrefs.GetInt("SpeedUp", 0);
        invisibility = PlayerPrefs.GetInt("Invisibility", 0);
        navigation = PlayerPrefs.GetInt("Navigation", 0);

        Debug.Log($"Loaded Power-ups: Freeze={freeze}, SpeedUp={speedup}, Invisibility={invisibility}, Navigation={navigation}");
        
        // Notify any listeners that the power-ups have been loaded or updated
        NotifyPowerupsUpdated();
    }

    private IEnumerator LoadPowerupsEverySecond()
    {
        while (true)
        {
            LoadPowerups();
            yield return new WaitForSeconds(1f);
        }
    }

    // Notify subscribers that power-ups have been updated
    private void NotifyPowerupsUpdated()
    {
        OnPowerupUpdated?.Invoke();
    }

    public delegate void PowerupUpdated();
    public event PowerupUpdated OnPowerupUpdated;
}
