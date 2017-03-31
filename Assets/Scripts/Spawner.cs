using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public Wave[] waves;
	public Enemy enemy;

	public event System.Action<int, int> OnNewWave;

	Wave currentWave;
	int currentWaveNumber;

	int enemiesToSpawn;
	int enemiesAlive;
	float nextSpawnTime;
	Transform player;
	public Transform[] spawnPoints;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		NextWave ();
	}

	void Update() {
		if (enemiesToSpawn > 0 && Time.time > nextSpawnTime) {
			enemiesToSpawn--;
			nextSpawnTime = Time.time + currentWave.timeBetweenSpawn;
			int spawnPointIndex = Random.Range(0, spawnPoints.Length);
			Enemy spawnEnemy = Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as Enemy;
			spawnEnemy.transform.LookAt(player);
			spawnEnemy.OnDeath += OnEnemyDeath;
		}
	}

	void NextWave () {
		currentWaveNumber ++;
		if (currentWaveNumber - 1 < waves.Length) {
			currentWave = waves [currentWaveNumber-1];
			enemiesToSpawn = currentWave.enemyCount;
			enemiesAlive = enemiesToSpawn;
			AudioManager.instance.PlaySound ("Next Level", player.transform.position);
			if (OnNewWave != null) {
				OnNewWave (currentWaveNumber, enemiesToSpawn);
			}
		}
	}

	void OnEnemyDeath() {
		//print("Enemy Died")
		enemiesAlive--;
		if (enemiesAlive == 0) {
			NextWave();
		}
	}

	[System.Serializable]
	public class Wave {
		public int enemyCount;
		public float timeBetweenSpawn;
	}
}
