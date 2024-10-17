using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.AI;

public class Freeze : MonoBehaviour
{
    public NavMeshAgent zombieAgent;
    public float freezeDuration = 10f;
    public Button freezeButton;
    Animator animator;
    private string username;

    private void Start()
    {
        // Retrieve the logged-in username from PlayerPrefs
        username = PlayerPrefs.GetString("LoggedInUser", null);

        if (string.IsNullOrEmpty(username))
        {
            Debug.LogError("No logged-in user found");
        }
    }

    public void OnActivation()
    {
        StartCoroutine(CheckAndActivateFreeze());
    }

    private IEnumerator CheckAndActivateFreeze()
    {
        // Call the PHP backend to check and deduct freeze
        WWWForm form = new WWWForm();
        form.AddField("username", username);  

        using (UnityWebRequest www = UnityWebRequest.Post("http://192.168.1.248/UnityFindME/freeze.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Network/Protocol Error: " + www.error);
            }
            else
            {
                // Log the raw response for debugging purposes
                string response = www.downloadHandler.text;
                Debug.Log("Server response: " + response);

                try
                {
                    FreezeResponse freezeResponse = JsonUtility.FromJson<FreezeResponse>(response);

                    if (freezeResponse.status == "success")
                    {
                        // Activate freeze effect
                        animator = GetComponent<Animator>();
                        StartCoroutine(FreezeZombie(zombieAgent));
                        StartCoroutine(ButtonCooldownRoutine());
                        Debug.Log("Freeze activated, remaining count: " + freezeResponse.freeze_count);
                    }
                    else
                    {
                        // Show error message
                        Debug.LogError("Error from server: " + freezeResponse.message);
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("JSON parse error: " + ex.Message);
                }
            }
        }
    }

    private IEnumerator FreezeZombie(NavMeshAgent zombieAgent)
    {
        zombieAgent.isStopped = true;
        Debug.Log("Zombie frozen!");
        
        yield return new WaitForSeconds(freezeDuration);
        zombieAgent.isStopped = false;
        Debug.Log("Zombie unfrozen!");
    }

    private IEnumerator ButtonCooldownRoutine()
    {
        freezeButton.interactable = false;
        yield return new WaitForSeconds(freezeDuration);
        freezeButton.interactable = true;
    }

    [System.Serializable]
    public class FreezeResponse
    {
        public string status;
        public string message;
        public int freeze_count;
    }
}
