using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject enemyClone;
	public Transform enemyLocation;

	private float spawnCd;
	private float spawnRandValue;
	public float spawnRate;
	public float spawnCdTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Spawn ();
	}
	void Spawn()
	{
		spawnCd -= Time.deltaTime;
		if (spawnCd <= 0) {
			spawnRandValue = Random.value;
			spawnCd = spawnCdTime;
			if (spawnRandValue < spawnRate) {
				Instantiate (enemyClone, enemyLocation.position, enemyLocation.rotation);
			}
		}
	}
}
