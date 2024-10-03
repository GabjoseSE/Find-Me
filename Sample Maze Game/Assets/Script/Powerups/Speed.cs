using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speed : MonoBehaviour
{
    public FirstPersonController playerController;
    public Button speedBoostButton;
    public float boostSpeed = 100f;
    public float boostDuration = 8f;
    private float remainingBoostTime;

    void Start()
    {
        if (playerController == null)
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        }
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
        float originalSpeed = playerController.moveSpeed;
        playerController.SetMoveSpeed(boostSpeed);
        Debug.Log("YOU ARE SPEED!!");
        yield return new WaitForSeconds(boostDuration);
        Debug.Log("YOU ARE SPEED NO LONGER");
        playerController.SetMoveSpeed(originalSpeed);
    }
    private IEnumerator ButtonCooldownRoutine()
    {
        speedBoostButton.interactable = false;
        yield return new WaitForSeconds(boostDuration);
        speedBoostButton.interactable = true;
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
