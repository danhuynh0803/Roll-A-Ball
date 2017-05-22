using UnityEngine;
using System.Collections;

public class SpawnController2 : MonoBehaviour {

	public float spawnTimer; 											// Time till next spawn
	public float minX, maxX, minZ, maxZ;								// Spawn boundaries
	public float spawnHeight;											// Height where objects are spawned

	public GameObject pickup;
	public GameObject speedup;
	public GameObject slowdown;

	private LevelController2 levelController;
	private float currTimer; 											// Current time till next spawn
	// Use this for initialization
	void Start () {
		levelController = FindObjectOfType<LevelController2> ();
		currTimer = spawnTimer;
	}
	
	// Update is called once per frame
	void Update () {
		currTimer -= Time.deltaTime;
		if (currTimer <= 0.0f) { 										// Spawn a random pickup, but give more weight to the normal pickup
			currTimer = spawnTimer;
			float rand = Random.Range (0.0f, 1.0f);
			if (rand < 0.10) { 
				// Spawn speedup
				Instantiate(speedup, new Vector3(Random.Range(minX, maxX), spawnHeight, Random.Range(minZ, maxZ)), Quaternion.identity);
			} else if (rand > 0.10 && rand < 0.25) { 
				// Spawn slowdown
				Instantiate(slowdown, new Vector3(Random.Range(minX, maxX), spawnHeight, Random.Range(minZ, maxZ)), Quaternion.identity);
			} else { 
				// Spawn normal
				if (!levelController.hadOvertime)
					Instantiate(pickup, new Vector3(Random.Range(minX, maxX), spawnHeight, Random.Range(minZ, maxZ)), Quaternion.identity);			
			}
		}
	}
}
