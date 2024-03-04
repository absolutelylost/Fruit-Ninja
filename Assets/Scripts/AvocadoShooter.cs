using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvocadoShooter : MonoBehaviour
{
	[SerializeField] private float timer = 0f;
    //time since last bullet firing
	private float delayTimer = 7.0f;

	private float bulletTime = 5f;

    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float enemySpeed;
    public bool isGrabbed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		isGrabbed = GetComponent<FruitBehavior>().isGrabbed;

		if (!isGrabbed)
        {
			timer += Time.deltaTime;
			if (timer >= delayTimer)
			{
				ShootAtPlayer();
				//reset timer
				timer = 0;
			}
		}
	}

    void ShootAtPlayer()
    {

        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(-bulletRig.transform.up * enemySpeed);
        Destroy(bulletObj, 5f);
    }
}
