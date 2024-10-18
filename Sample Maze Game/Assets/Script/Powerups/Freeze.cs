using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Freeze : MonoBehaviour
{
    public ZombieAI zombieBehaviour;
    public NavMeshAgent zombieAgent;
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

        if (zombieBehaviour == null)
        {
            zombieBehaviour = GameObject.FindWithTag("Zombie").GetComponent<ZombieAI>();
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
        float originalDetect = zombieBehaviour.detectionRange;
        float originalAttack = zombieBehaviour.attackRange;
        zombieBehaviour.SetAttackRange(invisAttack);
        zombieBehaviour.SetDetectionRange(invisDetect);
        zombieAgent.isStopped = true;
        Debug.Log("Zombie frozen!");

        yield return new WaitForSeconds(freezeDuration);
        zombieBehaviour.SetAttackRange(originalAttack);
        zombieBehaviour.SetDetectionRange(originalDetect);
        zombieAgent.isStopped = false;
        Debug.Log("Zombie unfrozen!");

    }
    private IEnumerator ButtonCooldownRoutine()
    {
        freezeButton.interactable = false;
        float elapsedTime = 0f;
        while (elapsedTime < freezeDuration)
        {
            elapsedTime += Time.deltaTime;
            cooldownImage.fillAmount = elapsedTime / freezeDuration; // **Highlighted: Updates the fill amount of the image**
            yield return null;
        }
        freezeButton.interactable = true;
        cooldownImage.fillAmount = 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NavMeshAgent zombieAgent = GetComponent<NavMeshAgent>();
            if (zombieAgent != null)
            {
                StartCoroutine(FreezeZombie(zombieAgent));
            }
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    public class FreezeResponse
    {
        public string status;
        public string message;
        public int freeze_count;
    }
}