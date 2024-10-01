using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Invisible : MonoBehaviour
{

    public Collider playerCollider;
    public string enemyTag = "Zombie";
    public float IgnoreCollisionDuration = 10f;
    private bool isIgnoringCollisions = false;
    void Start()
    {
        if (playerCollider == null)
        {
            playerCollider = GetComponent<Collider>();
        }
    }
    public bool isPlayerInvisible { get; private set; }
    public void IgnoreUnitCollisionForTime()
    {
        if (!isIgnoringCollisions)
        {
            StartCoroutine(IgnoreCollisionsTemporarily());
            Debug.Log("You're Now INVISIBLE.");
            isPlayerInvisible = true;
            ZombieAI zombieAI = GameObject.FindObjectOfType<ZombieAI>();
            if (zombieAI != null)
            {
            zombieAI.isChasingPlayer = false;
            zombieAI.agent.SetDestination(zombieAI.transform.position);
            }
        }
    }
    private IEnumerator IgnoreCollisionsTemporarily()
{
    isIgnoringCollisions = true;
    GameObject[] units = GameObject.FindGameObjectsWithTag(enemyTag);
    foreach (GameObject unit in units)
    {
        Collider unitCollider = unit.GetComponent<Collider>();
        if (unitCollider != null)
        {
            Physics.IgnoreCollision(playerCollider, unitCollider, true);
        }
    }
    yield return new WaitForSeconds(IgnoreCollisionDuration);
    foreach (GameObject unit in units)
    {
        Collider unitCollider = unit.GetComponent<Collider>();
        if (unitCollider != null)
        {
            Physics.IgnoreCollision(playerCollider, unitCollider, false);
        }
    }
    isIgnoringCollisions = false;
    isPlayerInvisible = false;

    // Re-enable the NavMeshAgent
    NavMeshAgent agent = GetComponent<NavMeshAgent>();
    if (agent != null)
    {
        agent.enabled = true;
    }
}

    //For when picking up a powerup NOT FOR BUTTONS
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IgnoreUnitCollisionForTime();
            Debug.Log("YOU ARE NOW INVISIBLE!!!");
        }
    }
}
