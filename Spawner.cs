using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject enemyClone;
	public Transform enemyLocation;

	private float spawnCd;
	private float spawnChance;
	public float spawnRate;
	public float spawnCDTime;
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
		spawnChance = Random.value * Time.deltaTime;
		if (spawnCd <= 0) {
			if (spawnChance < spawnRate) {
				Instantiate (enemyClone, enemyLocation.position, enemyLocation.rotation);
				spawnCd = spawnCDTime;
			}
		}
	}
}
