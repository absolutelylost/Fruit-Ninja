using UnityEngine;

public class SweetCollisionHandler : MonoBehaviour
{
    public int scoreValue = 10; // Score value to add when object is destroyed

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided GameObject has a specific tag or name
        if (collision.gameObject.CompareTag("Collectable"))
        {
            // Destroy the collided GameObject
            Destroy(collision.gameObject);
        }
    }
}
