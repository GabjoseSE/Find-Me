using UnityEngine;

public class DatabasePathLogger : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Database Path: " + Application.persistentDataPath);
    }
}
