using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Freeze : MonoBehaviour
{
    public NavMeshAgent zombieAgent;
    public float freezeDuration = 10f;
    public Button freezeButton;

    public void OnActivation()
    {
        StartCoroutine(FreezeZombie(zombieAgent));
        StartCoroutine(ButtonCooldownRoutine());

    }
    private IEnumerator FreezeZombie(NavMeshAgent zombieAgent)
    {
        zombieAgent.isStopped = true;
        Debug.Log("Zombie frozen!");
        yield return new WaitForSeconds(freezeDuration);
        zombieAgent.isStopped = false;
        Debug.Log("Zombie unfrozen!");
    }
    private IEnumerator ButtonCooldownRoutine()
    {
        freezeButton.interactable = false;
        yield return new WaitForSeconds(freezeDuration);
        freezeButton.interactable = true;
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