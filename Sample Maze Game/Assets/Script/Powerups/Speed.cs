using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Speed : MonoBehaviour
{
    public PlayerMove playerController;
    public Button speedBoostButton;
    public float boostSpeed = 100f;
    public float boostDuration = 8f;
    private float remainingBoostTime;

    //timer
    public Image cooldownImage;
    private string username;

    void Start()
    {
        username = PlayerPrefs.GetString("LoggedInUser", null);

        if (string.IsNullOrEmpty(username))
        {
            Debug.LogError("No logged-in user found");
        }

        if (playerController == null)
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        }

        //timer
        cooldownImage.fillAmount = 1;
    }
    public void OnActivation()
    {
        StartCoroutine(CheckAndActivateSpeedup());
    }
    private IEnumerator CheckAndActivateSpeedup()
    {
        // Call the PHP backend to check and deduct speedup power-up
        WWWForm form = new WWWForm();
        form.AddField("username", username);  

        using (UnityWebRequest www = UnityWebRequest.Post("http://192.168.43.237/UnityFindME/speedup.php", form))
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
                SpeedupResponse speedupResponse = JsonUtility.FromJson<SpeedupResponse>(response);

                if (speedupResponse.status == "success")
                {
                    // Activate speedup effect
                    
                    StartCoroutine(ApplySpeedBoost());
                    StartCoroutine(ButtonCooldownRoutine());
                    Debug.Log("8 Seconds Boost");
            Debug.Log("8 Seconds Button Cooldown");
                    Debug.Log("Speedup activated, remaining count: " + speedupResponse.speedup_count);
                }
                else
                {
                    // Show error message
                    Debug.LogError(speedupResponse.message);
                }
            }
        }
    }

    private IEnumerator ApplySpeedBoost()
    {
        float originalSpeed = playerController.SpeedMove;
        playerController.SetMoveSpeed(boostSpeed);
        Debug.Log("YOU ARE SPEED!!");
        yield return new WaitForSeconds(boostDuration);
        Debug.Log("YOU ARE SPEED NO LONGER");
        playerController.SetMoveSpeed(originalSpeed);
    }
    private IEnumerator ButtonCooldownRoutine()
    {
        speedBoostButton.interactable = false;
        float elapsedTime = 0f;

        // **Highlighted: Start cooldown and update fill amount**
        while (elapsedTime < boostDuration)
        {
            elapsedTime += Time.deltaTime;
            cooldownImage.fillAmount = elapsedTime / boostDuration; // **Highlighted: Updates the fill amount of the image**
            yield return null;
        }

        speedBoostButton.interactable = true;
        cooldownImage.fillAmount = 1; // **Highlighted: Reset fill amount after cooldown**

        //old code backup
        /**speedBoostButton.interactable = false;
        yield return new WaitForSeconds(boostDuration);
        speedBoostButton.interactable = true;**/
    }

    //For powerups NOT FOR BUTTONS

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ApplySpeedBoost());
            Destroy(gameObject);
        }
    }
    [System.Serializable]
    public class SpeedupResponse
    {
        public string status;
        public string message;
        public int speedup_count;
    }

}
