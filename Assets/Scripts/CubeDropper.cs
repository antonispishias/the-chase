using UnityEngine;
using System.Collections;

public class CubeDropper : MonoBehaviour {

	public DropCube cube;
	public float timeBetweenSpawn;
	float nextSpawnTime;
	public Transform[] spawnPoints;
	bool[] spawned;

	void Start() {
		spawned = new bool[spawnPoints.Length];
	}

	void Update() {
		var pcCount = GameObject.FindObjectsOfType<DropCube>().Length;
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
			DropCube c = Instantiate (cube, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation) as DropCube;
			c.transform.parent = spawnPoints[spawnPointIndex].transform;
		}

	}

}