using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform[] waypoints;        // Waypoints for patrolling
    public Transform player;             // Reference to the player
    public float detectionRange = 10f;   // Range within which the zombie can detect the player
    public float attackRange = 2f;       // Distance from which the zombie can attack the player
    public float attackCooldown = 3f;    // Time between attacks

    private int currentWaypoint = 0;
    public NavMeshAgent agent;
    public bool isChasingPlayer = false;
    private float lastAttackTime;
    private Invisible playerInvisibleScript;

    void Start()
{
    agent = GetComponent<NavMeshAgent>();
    playerInvisibleScript = player.GetComponent<Invisible>();

    if (playerInvisibleScript == null)
    {
        Debug.LogError("Invisible script not found on player!");
        PatrolToNextWaypoint();
        return;
    }

    
    
}

    void Update()
    {
      
        // Distance to the player
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (playerInvisibleScript != null)
        {
            if (distanceToPlayer < detectionRange && !playerInvisibleScript.isPlayerInvisible)
            {
                // If the player is within detection range, chase the player
                isChasingPlayer = true;
            }
            else
            {
                // If the player is not detected or is invisible, stop chasing
                isChasingPlayer = false;
                agent.SetDestination(transform.position); // Stop chasing the player
            }
        }
        else
        {
            // If the playerInvisibleScript is null, assume the player is always visible
            if (distanceToPlayer < detectionRange)
            {
                // If the player is within detection range, chase the player
                isChasingPlayer = true;
            }
            else
            {
                // If the player is not detected, stop chasing
                isChasingPlayer = false;
                agent.SetDestination(transform.position); // Stop chasing the player
            }
        }
        if (isChasingPlayer)
        {
            // Chase the player
            ChasePlayer();

            // Attack if within attack range
            if (distanceToPlayer < attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            // Continue patrolling if not chasing the player
            Patrol();
        }
    }

    // Patrol to the next waypoint
    void Patrol()
{
    if (agent != null && agent.isActiveAndEnabled)
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            PatrolToNextWaypoint();
        }
    }
    else
    {
        Debug.LogError("NavMeshAgent is null or inactive!");
    }
}

    // Move to the next waypoint in the patrol route
    void PatrolToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        agent.destination = waypoints[currentWaypoint].position;
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
    }

    // Chase the player
    public void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    // Attack the player if within range
    void AttackPlayer()
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            // Attack the player (this could be playing an animation or dealing damage)
            Debug.Log("Zombie attacks!");

            // Reset attack timer
            lastAttackTime = Time.time;
        }
    }

    // Visualize detection and attack range in the editor
    private void OnDrawGizmosSelected()
    {
        // Detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public void SetAttackRange(float newRange)
    {
        attackRange = newRange;
    }
}
