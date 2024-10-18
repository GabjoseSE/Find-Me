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
    public int damage = 1;               // Amount of damage zombie deals to player

    private int currentWaypoint = 0;
    private NavMeshAgent agent;
    private bool isChasingPlayer = false;
    private float lastAttackTime;
    private Animator animator;
    private PlayerHealth playerHealth;   // Reference to player's health system

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth = FindObjectOfType<PlayerHealth>();

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
            // If within attack range, attack the player continuously
            if (distanceToPlayer < attackRange)
            {
                // Attack the player every attack cooldown period
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    AttackPlayer();
                    lastAttackTime = Time.time; // Reset last attack time
                }
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
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log("Zombie attacks! Player takes damage.");
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

    // Setters for attack, detection range, and speed
    public void SetAttackRange(float newAttack)
    {
        attackRange = newAttack;
    }

    public void SetDetectionRange(float newDetection)
    {
        detectionRange = newDetection;
    }

    public void SetSpeed(float newSpeed)
    {
        chaseSpeed = newSpeed;
    }
}
