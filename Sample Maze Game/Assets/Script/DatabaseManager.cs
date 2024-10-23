using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DatabaseManager : MonoBehaviour
{
    // Register a new player
    public IEnumerator RegisterPlayer(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://192.168.1.248/UnityFindME/add_player.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Debug.Log("Player registered: " + www.downloadHandler.text);
            }
        }
    }

    // Fetch player data by username
    public IEnumerator GetPlayerData(string username)
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://192.168.1.248/UnityFindME/get_player.php?username=" + username))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Debug.Log("Player Data: " + www.downloadHandler.text);
            }
        }
    }
}
