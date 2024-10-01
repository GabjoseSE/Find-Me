using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    public ZombieAI zomBehaviour;
    public float zombFreeze = 0f;
    public float freezeDuration = 10f;
    void Start()
    {
        if (zomBehaviour == null)
        {
            zomBehaviour = GameObject.FindWithTag("Zombie").GetComponent<ZombieAI>();
        }
    }
    public void OnActivation()
    {
        StartCoroutine(ApplyFreeze());
    }
    private IEnumerator ApplyFreeze()
    {
        float originalRadius = zomBehaviour.attackRange;
        zomBehaviour.SetAttackRange(zombFreeze);
        yield return new WaitForSeconds(freezeDuration);
        zomBehaviour.SetAttackRange(originalRadius);
    }
    //For powerups not for button
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ApplyFreeze());
            Destroy(gameObject);
        }
    }
}
