using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class runspeed : StateMachineBehaviour
{
    // The base movement speed for running (adjust this based on how fast you want the zombie to run)
    public float baseSpeed = 3f;

    // The step cycle time for running (0.13 seconds as per your requirement)
    public float stepCycleTime = 0.13f;

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get the GameObject the Animator is attached to
        GameObject zombie = animator.gameObject;

        // Get the velocity (assuming a NavMeshAgent is controlling the zombie's movement)
        NavMeshAgent navAgent = zombie.GetComponent<NavMeshAgent>();

        if (navAgent != null)
        {
            // Calculate the speed based on the velocity of the NavMeshAgent
            float currentSpeed = navAgent.velocity.magnitude;

            // Adjust the animation speed to match the running movement speed
            animator.speed = currentSpeed / baseSpeed;

            // Optional: log to check the values during testing
            // Debug.Log("Current Speed: " + currentSpeed + ", Animation Speed: " + animator.speed);
        }
    }
}
