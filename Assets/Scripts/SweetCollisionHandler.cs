using UnityEngine;
using UnityEngine.Rendering;

public class SweetCollisionHandler : MonoBehaviour
{
    public int scoreValue = 10; // Score value to add when object is destroyed

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided GameObject has a specific tag or name
        if (collision.gameObject.CompareTag("Collectable"))
        {
            Debug.Log("Am I colliding?");
            // Destroy the collided GameObject
            Destroy(collision.gameObject);
        }
    }
}
