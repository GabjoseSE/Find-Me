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
    private NavMeshAgent agent;
    private bool isChasingPlayer = false;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        PatrolToNextWaypoint();
    }

    void Update()
    {
        // Distance to the player
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer < detectionRange)
        {
            // If the player is within detection range, chase the player
            isChasingPlayer = true;
        }
        else
        {
            // If the player is not detected, continue patrolling
            isChasingPlayer = false;
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
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            PatrolToNextWaypoint();
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
    void ChasePlayer()
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
