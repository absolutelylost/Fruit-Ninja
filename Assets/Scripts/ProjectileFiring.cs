using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFiring : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform firePoint; // Point from which the projectile will be fired
    public float projectileForce = 500f; // Force applied to the projectile

    // Update is called once per frame
    void Update()
    {
        // Check for input to fire the projectile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        // Instantiate the projectile at the fire point
        GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody component of the projectile
        Rigidbody projectileRigidbody = projectileInstance.GetComponent<Rigidbody>();

        // Apply force to the projectile to launch it towards the XR Rig camera
        if (projectileRigidbody != null)
        {
            projectileRigidbody.AddForce(projectileForce * firePoint.forward);
        }
    }
}
