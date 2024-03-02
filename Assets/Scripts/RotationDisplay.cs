using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationDisplay : MonoBehaviour
{
    private float rotationSpeed = 50.0f;
    public bool itemPresent = false;
    private float spawnTime = 10.0f;
    private float timeSinceGone = 0.0f;
    [SerializeField] private GameObject spawnablePrefab;

    // Start is called before the first frame update
    void Start()
    {
		spawnTime = Random.Range(10, 45);
        Debug.Log(gameObject.name + " spawn interval will be: " + spawnTime + " seconds.");
	}

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        // if sweet not present, allow timer to start
        if(gameObject.transform.childCount == 0)
        {
            itemPresent = false;
		}

        if (!itemPresent)
        {
            if(timeSinceGone >= spawnTime)
            {
                itemPresent = true;
                GameObject instanceObject = Instantiate(spawnablePrefab, Vector3.zero, spawnablePrefab.transform.rotation);
                instanceObject.transform.parent = transform;
				instanceObject.transform.localPosition = Vector3.zero;

				//reset timer
				timeSinceGone = 0;
            }

            timeSinceGone += Time.deltaTime;
        }
    }
}
