using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementComponent : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component
    public LayerMask obstacleMask; // Layer mask to define obstacles that block line of sight
    public float lineOfSightDistance = 10f; // Maximum distance for line of sight check

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // Get reference to NavMeshAgent component
    }

    void Update()
    {
        // Check if player reference is not null and the player is in line of sight
        if (player != null && CanSeePlayer())
        {
            // Set the destination of the NavMeshAgent to the player's position
            navMeshAgent.SetDestination(player.position);
        }
    }

    bool CanSeePlayer()
    {
        // Calculate direction to the player
        Vector3 directionToPlayer = player.position - transform.position;

        // Log directionToPlayer and its magnitude to the console

        // Check if player is within line of sight distance
        if (directionToPlayer.magnitude <= lineOfSightDistance)
        {
            // Perform a raycast to check for obstacles between this object and the player
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, lineOfSightDistance, obstacleMask))
            {
                // If an obstacle is hit, the player is not in line of sight
                if (hit.collider.gameObject.CompareTag("Obstacle"))
                {
                    Debug.Log("I got here first!!!!!");
                    return false; // Player is in line of sight
                }
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("I got here finally!!!!!");
                    return true; // Player is in line of sight
                }
            }
        }

        return false; // Player is not in line of sight or too far away
    }
}
