using UnityEngine;
using System.Collections;

public class CubeSpawner : MonoBehaviour {

	public PowerCube pc;
	public float timeBetweenSpawn;
	float nextSpawnTime;
	public Transform[] spawnPoints;
	bool[] spawned;

	void Start() {
		spawned = new bool[spawnPoints.Length];
	}

	void Update() {
		var pcCount = GameObject.FindGameObjectsWithTag ("PowerCube").Length;
		for (int i =0; i<spawnPoints.Length; i++) {
			if (spawnPoints[i].childCount > 0) {
				spawned[i] = true;
			}
			else {
				spawned[i] = false;
			}
		} 

		if (pcCount < spawnPoints.Length && Time.time > nextSpawnTime) {
			nextSpawnTime = Time.time + timeBetweenSpawn;
			int spawnPointIndex;
			do {
				spawnPointIndex = Random.Range (0, spawnPoints.Length);
			} while (spawned[spawnPointIndex]);
			PowerCube p = Instantiate (pc, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation) as PowerCube;
			p.transform.parent = spawnPoints[spawnPointIndex].transform;
		}

	}
	
}
