using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public FirstPersonController playerController;
    public float boostSpeed = 100f;
    public float boostDuration = 8f;
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
    }

    private IEnumerator ApplySpeedBoost()
    {
        float originalSpeed = playerController.moveSpeed;
        playerController.SetMoveSpeed(boostSpeed);
        yield return new WaitForSeconds(boostDuration);
        playerController.SetMoveSpeed(originalSpeed);
    }

    //For powerups NOT FOR BUTTONS

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ApplySpeedBoost());
            Destroy(gameObject); // Destroy the powerup after use
        }
    }

}
