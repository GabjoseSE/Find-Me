using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Invisible : MonoBehaviour
{
    public ZombieAI zombieBehaviour;
    public float invisDetect = 0f;
    public float invisAttack = 0f;
    public float invisibilityDuration = 10f;
    public Button invisButton;
    public Image cooldownImage;

    void Start()
    {
        if (zombieBehaviour == null)
        {
            zombieBehaviour = GameObject.FindWithTag("Zombie").GetComponent<ZombieAI>();
        }
        cooldownImage.fillAmount = 1;
    }
    public void OnActivation()
    {
        StartCoroutine(ActivateInvisibility());
        StartCoroutine(ButtonCooldownRoutine());
        Debug.Log("You're Now INVISIBLE.");
    }
    private IEnumerator ActivateInvisibility()
    {
        float originalDetect = zombieBehaviour.detectionRange;
        float originalAttack = zombieBehaviour.attackRange;
        zombieBehaviour.SetAttackRange(invisAttack);
        zombieBehaviour.SetDetectionRange(invisDetect);
        Debug.Log("You are now Invisible");
        yield return new WaitForSeconds(invisibilityDuration);
        Debug.Log("You are no longer Invisible");
        zombieBehaviour.SetAttackRange(originalAttack);
        zombieBehaviour.SetDetectionRange(originalDetect);

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


    //For when picking up a powerup NOT FOR BUTTONS
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActivateInvisibility();
            Debug.Log("YOU ARE NOW INVISIBLE!!!");
        }
    }
}
