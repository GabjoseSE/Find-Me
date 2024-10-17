using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speed : MonoBehaviour
{
    public PlayerMove playerController;
    public Button speedBoostButton;
    public float boostSpeed = 100f;
    public float boostDuration = 8f;
    private float remainingBoostTime;

    //timer
    public Image cooldownImage;

    void Start()
    {
        if (playerController == null)
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        }

        //timer
        cooldownImage.fillAmount = 1;
    }
    public void OnActivation()
    {
        StartCoroutine(ApplySpeedBoost());
        StartCoroutine(ButtonCooldownRoutine());
        Debug.Log("8 Seconds Boost");
        Debug.Log("8 Seconds Button Cooldown");
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

}
