using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Freeze : MonoBehaviour
{
    public Button invisBTN;
    public ZombieAI[] zombieBehaviours; // Array of ZombieAI
    public NavMeshAgent[] zombieAgents; // Array of NavMeshAgents
    public float freezeDuration = 10f;
    public Button freezeButton;
    public float invisDetect = 0f;
    public float invisAttack = 0f;
    private List<Animator> zombieAnimators = new List<Animator>();
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

        //Find and store all the Animators in the zombies
        foreach (var zombieBehaviour in zombieBehaviours)
        {
            Animator anim = zombieBehaviour.GetComponent<Animator>();
            if (anim != null)
            {
                zombieAnimators.Add(anim);
            }
        }

        cooldownImage.fillAmount = 1;
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

        using (UnityWebRequest www = UnityWebRequest.Post("http://192.168.41.90/UnityFindME/freeze.php", form))
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
        List<float> originalAnimatorSpeeds = new List<float>();

        // Freeze all zombies
        for (int i = 0; i < zombieBehaviours.Length; i++)
        {
            ZombieAI zombieBehaviour = zombieBehaviours[i];
            NavMeshAgent zombieAgent = zombieAgents[i];
            Animator zombieAnimator = zombieAnimators[i];

            // Save original detection and attack ranges
            originalDetectRanges.Add(zombieBehaviour.detectionRange);
            originalAttackRanges.Add(zombieBehaviour.attackRange);

            if (zombieAnimator != null)
            {
                originalAnimatorSpeeds.Add(zombieAnimator.speed);
                zombieAnimator.speed = 0; // Freeze animation
            }

            // Set zombie AI to freeze mode
            zombieBehaviour.SetAttackRange(invisAttack);
            zombieBehaviour.SetDetectionRange(invisDetect);
            zombieAgent.isStopped = true;
        }
        invisBTN.interactable = false;

        Debug.Log("All zombies frozen!");

        yield return new WaitForSeconds(freezeDuration);

        Debug.Log("All zombies unfrozen!");
        invisBTN.interactable = true;

        // Restore original detection and attack ranges for all zombies
        for (int i = 0; i < zombieBehaviours.Length; i++)
        {
            zombieBehaviours[i].SetAttackRange(originalAttackRanges[i]);
            zombieBehaviours[i].SetDetectionRange(originalDetectRanges[i]);
            zombieAgents[i].isStopped = false;

            if (zombieAnimators[i] != null)
            {
                zombieAnimators[i].speed = originalAnimatorSpeeds[i];
            }
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
