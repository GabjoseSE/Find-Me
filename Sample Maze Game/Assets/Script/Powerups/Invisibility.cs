using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public void IgnoreUnitCollisionForTime()
    {
        if (!isIgnoringCollisions)
        {
            StartCoroutine(IgnoreCollisionsTemporarily());
            Debug.Log("You're Now INVISIBLE.");
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
