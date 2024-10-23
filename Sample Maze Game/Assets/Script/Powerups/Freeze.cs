using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Freeze : MonoBehaviour
{
    public ZombieAI[] zombieBehaviours; // Array of ZombieAI
    public NavMeshAgent[] zombieAgents; // Array of NavMeshAgents
    public float freezeDuration = 10f;
    public Button freezeButton;
    public float invisDetect = 0f;
    public float invisAttack = 0f;
    Animator animator;
    public Image cooldownImage;
    private string username;

    void Start()
    {
        // Retrieve the logged-in username from PlayerPrefs
        username = PlayerPrefs.GetString("LoggedInUser", null);

        if (string.IsNullOrEmpty(username))
        {
            Debug.LogError("No logged-in user found");
        }

        if (zombieBehaviours == null || zombieBehaviours.Length == 0)
        {
            // Find all zombies with the ZombieAI component
            zombieBehaviours = GameObject.FindObjectsOfType<ZombieAI>();
        }

        if (zombieAgents == null || zombieAgents.Length == 0)
        {
            // Find all zombies with the NavMeshAgent component
            zombieAgents = GameObject.FindObjectsOfType<NavMeshAgent>();
        }

        cooldownImage.fillAmount = 1;
    }

    public void OnActivation()
    {
        StartCoroutine(CheckAndActivateFreeze());
        animator = GetComponent<Animator>();
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
                        // Activate freeze effect for all zombies
                        animator = GetComponent<Animator>();
                        StartCoroutine(FreezeZombies());
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

    private IEnumerator FreezeZombies()
    {
        List<float> originalDetectRanges = new List<float>();
        List<float> originalAttackRanges = new List<float>();

        // Freeze all zombies
        for (int i = 0; i < zombieBehaviours.Length; i++)
        {
            ZombieAI zombieBehaviour = zombieBehaviours[i];
            NavMeshAgent zombieAgent = zombieAgents[i];

            // Save original detection and attack ranges
            originalDetectRanges.Add(zombieBehaviour.detectionRange);
            originalAttackRanges.Add(zombieBehaviour.attackRange);

            // Set zombie AI to freeze mode
            zombieBehaviour.SetAttackRange(invisAttack);
            zombieBehaviour.SetDetectionRange(invisDetect);
            zombieAgent.isStopped = true;
        }

        Debug.Log("All zombies frozen!");

        yield return new WaitForSeconds(freezeDuration);

        Debug.Log("All zombies unfrozen!");

        // Restore original detection and attack ranges for all zombies
        for (int i = 0; i < zombieBehaviours.Length; i++)
        {
            zombieBehaviours[i].SetAttackRange(originalAttackRanges[i]);
            zombieBehaviours[i].SetDetectionRange(originalDetectRanges[i]);
            zombieAgents[i].isStopped = false;
        }
    }

    private IEnumerator ButtonCooldownRoutine()
    {
        freezeButton.interactable = false;
        float elapsedTime = 0f;
        while (elapsedTime < freezeDuration)
        {
            elapsedTime += Time.deltaTime;
            cooldownImage.fillAmount = elapsedTime / freezeDuration; // Updates the fill amount of the image
            yield return null;
        }
        freezeButton.interactable = true;
        cooldownImage.fillAmount = 1;
    }

    [System.Serializable]
    public class FreezeResponse
    {
        public string status;
        public string message;
        public int freeze_count;
    }
}
