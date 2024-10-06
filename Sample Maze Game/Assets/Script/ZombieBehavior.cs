using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform[] waypoints;        // Waypoints for patrolling
    public Transform player;             // Reference to the player
    public float detectionRange = 10f;   // Range within which the zombie can detect the player
    public float attackRange = 2f;       // Distance from which the zombie can attack the player
    public float attackCooldown = 3f;    // Time between attacks
    public float patrolSpeed = 2f;       // Speed while patrolling
    public float chaseSpeed = 4f;        // Speed while chasing the player

    private int currentWaypoint = 0;
    private NavMeshAgent agent;
    private bool isChasingPlayer = false;
    private float lastAttackTime;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Set patrol speed and begin patrolling
        agent.speed = patrolSpeed;
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
            agent.speed = chaseSpeed;   // Increase speed when chasing
            animator.SetBool("Run", true);
        }
        else
        {
            // If the player is not detected, return to patrolling
            isChasingPlayer = false;
            agent.speed = patrolSpeed;  // Revert to patrol speed
            animator.SetBool("Run", false);
        }

        if (isChasingPlayer)
        {
            // If within attack range, stop moving and attack
            if (distanceToPlayer < attackRange)
            {
                AttackPlayer();
                animator.SetBool("Attack", true);
            }
            else
            {
                // Chase the player if not in attack range
                ChasePlayer();
                animator.SetBool("Attack", false);
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
        animator.SetBool("Walk", true);
    }

    // Chase the player
    void ChasePlayer()
    {
        // Set the zombie's destination to the player's position
        agent.SetDestination(player.position);
    }

    // Attack the player if within range
    void AttackPlayer()
    {


        if (Time.time - lastAttackTime > attackCooldown)
        {
            // Attack the player (this could be playing an animation or dealing damage)
            Debug.Log("Zombie attacks!");
            animator.SetBool("Attack", true);


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

    // Setters for attack and detection range
    public void SetAttackRange(float newAttack)
    {
        attackRange = newAttack;
    }

    public void SetDetectionRange(float newDetection)
    {
        detectionRange = newDetection;
    }
}
