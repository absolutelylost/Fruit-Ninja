using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lemonShooter : MonoBehaviour
{
	[SerializeField] private float timer = 0f;
	//time since last bullet firing
	private float delayTimer = 7.0f;

	private float bulletTime = 7f;

	public GameObject enemyBullet;
	public Transform spawnPoint1;
	public Transform spawnPoint2;
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

		GameObject bulletObj1 = Instantiate(enemyBullet, spawnPoint1.position, spawnPoint1.transform.rotation) as GameObject;
		GameObject bulletObj2 = Instantiate(enemyBullet, spawnPoint2.position, spawnPoint2.transform.rotation) as GameObject;
		Rigidbody bulletRig1 = bulletObj1.GetComponent<Rigidbody>();
		Rigidbody bulletRig2 = bulletObj2.GetComponent<Rigidbody>();
		bulletRig1.AddForce(-bulletObj1.transform.up * enemySpeed);
		bulletRig2.AddForce(-bulletObj2.transform.up * enemySpeed);
		Destroy(bulletObj1, bulletTime);
		Destroy(bulletObj2, bulletTime);
	}
}
