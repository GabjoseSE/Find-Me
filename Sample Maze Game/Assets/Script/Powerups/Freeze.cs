using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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

        animator = GetComponent<Animator>();
        StartCoroutine(FreezeZombie(zombieAgent));
        StartCoroutine(ButtonCooldownRoutine());


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
}