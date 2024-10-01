using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public FirstPersonController playerController;
    public float boostSpeed = 100f;
    public float boostDuration = 8f;
    private float remainingBoostTime;
    private bool isBoostActive = false;
    void Start()
    {
        if (playerController == null)
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        }
    }
    public void OnActivation()
    {
        if (!isBoostActive)
        {
            StartCoroutine(ApplySpeedBoost());
        }
        remainingBoostTime = boostDuration;
        Debug.Log("8 Seconds Boost");
    }

    private IEnumerator ApplySpeedBoost()
    {
        isBoostActive = true;
        float originalSpeed = playerController.moveSpeed;
        playerController.SetMoveSpeed(boostSpeed);
        while (remainingBoostTime > 0)
        {
            remainingBoostTime -= Time.deltaTime;
            yield return null;
        }
        playerController.SetMoveSpeed(originalSpeed);
        isBoostActive = false;
    }

    //For powerups NOT FOR BUTTONS

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isBoostActive)
            {
                StartCoroutine(ApplySpeedBoost());
            }
            remainingBoostTime = boostDuration;
            Destroy(gameObject); // Destroy the powerup after use
        }
    }

}
