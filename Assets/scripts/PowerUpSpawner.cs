using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Spawn PowerUp object when time passed.
 */
public class PowerUpSpawner : MonoBehaviour {

    public GameObject powerUp;
    public float spawnTime;
    public Transform spawnPoint;
    float nextSpawn = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	/*
     * Spawn PowerUp at the appointd time.
     */
	void Update () {
		if(Time.time > nextSpawn)
        {
            Debug.Log("Power spawned");
            nextSpawn = Time.time + spawnTime;
            Instantiate(powerUp, spawnPoint.position, spawnPoint.rotation);
        }
	}
}
