using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.AI;

public class Invisible : MonoBehaviour
{
    public ZombieAI[] zombieBehaviour;
    public float invisDetect = 0f;
    public float invisAttack = 0f;
    public float invisibilityDuration = 10f;
    public Button invisButton;
    public Image cooldownImage;

    private string username;

    void Start()
    {
        username = PlayerPrefs.GetString("LoggedInUser", null);

        if (string.IsNullOrEmpty(username))
        {
            Debug.LogError("No logged-in user found");
        }

        if (zombieBehaviour == null || zombieBehaviour.Length == 0)
        {
            zombieBehaviour = GameObject.FindObjectsOfType<ZombieAI>();
        }

        cooldownImage.fillAmount = 1;
    }

    public void OnActivation()
    {
        StartCoroutine(CheckAndActivateInvisibility());
    }

    private IEnumerator CheckAndActivateInvisibility()
    {
        // Call the PHP backend to check and deduct invisibility power-up
        WWWForm form = new WWWForm();
        form.AddField("username", username);

        using (UnityWebRequest www = UnityWebRequest.Post("http://192.168.1.248/UnityFindME/invisibility.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                // Parse the response
                string response = www.downloadHandler.text;
                InvisibilityResponse invisResponse = JsonUtility.FromJson<InvisibilityResponse>(response);

                if (invisResponse.status == "success")
                {
                    // Activate invisibility effect
                    StartCoroutine(ActivateInvisibility());
                    StartCoroutine(ButtonCooldownRoutine());
                    Debug.Log("Invisibility activated, remaining count: " + invisResponse.invisibility_count);
                }
                else
                {
                    // Show error message
                    Debug.LogError(invisResponse.message);
                }
            }
        }
    }

    private IEnumerator ActivateInvisibility()
    {
        // Save original detection and attack ranges for all zombies
        List<float> originalDetectRanges = new List<float>();
        List<float> originalAttackRanges = new List<float>();

        foreach (ZombieAI zombie in zombieBehaviour)
        {
            originalDetectRanges.Add(zombie.detectionRange);
            originalAttackRanges.Add(zombie.attackRange);

            // Set zombie AI to invisibility mode
            zombie.invisAttackRange(invisAttack);
            zombie.invisDetection(invisDetect);
        }

        Debug.Log("You are now Invisible");

        yield return new WaitForSeconds(invisibilityDuration);

        Debug.Log("You are no longer Invisible");

        // Restore original detection and attack ranges for all zombies
        for (int i = 0; i < zombieBehaviour.Length; i++)
        {
            zombieBehaviour[i].invisAttackRange(originalAttackRanges[i]);
            zombieBehaviour[i].invisDetection(originalDetectRanges[i]);
        }
    }

    private IEnumerator ButtonCooldownRoutine()
    {
        invisButton.interactable = false;
        float elapsedTime = 0f;

        while (elapsedTime < invisibilityDuration)
        {
            elapsedTime += Time.deltaTime;
            cooldownImage.fillAmount = elapsedTime / invisibilityDuration;
            yield return null;
        }

        invisButton.interactable = true;
        cooldownImage.fillAmount = 1;
    }

    [System.Serializable]
    public class InvisibilityResponse
    {
        public string status;
        public string message;
        public int invisibility_count;
    }
}
