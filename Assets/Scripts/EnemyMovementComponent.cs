using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementComponent : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // Get reference to NavMeshAgent component
    }

    void Update()
    {
        if (player != null) // Check if player reference is not null
        {
            // Set the destination of the NavMeshAgent to the player's position
            navMeshAgent.SetDestination(player.position);
        }
    }
}
