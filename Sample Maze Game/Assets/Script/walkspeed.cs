using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class walkspeed : StateMachineBehaviour
{
    // The base movement speed at which the animation plays normally (for example, 1 meter per second)
    public float baseSpeed = 1f;
    
    // The step cycle time (in seconds) for the animation (e.g., 2.06 seconds)
    public float stepCycleTime = 2.06f;

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

            // Adjust the animation speed to match the movement speed
            animator.speed = currentSpeed / baseSpeed;

            // Optional: log to check the values during testing
            // Debug.Log("Current Speed: " + currentSpeed + ", Animation Speed: " + animator.speed);
        }
    }
}
