using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public static int score { get; private set; }
	float lastKill;
	int streakCount;
	float streakExpiry = 1;

	void Start() {
		score = 0;
		Enemy.OnDeathStatic += OnEnemyKilled;
		DropCube.OnDeathStatic += OnCubeDestroyed;
		FindObjectOfType<Player> ().OnDeath += OnPlayerDeath;
		FindObjectOfType<Spawner>().OnNewWave += OnNewWave;


	}

	void OnNewWave(int w, int e) {
		if (w > 1) {
			score += 50*w;
		}
	}

	void OnCubeDestroyed() {
		score += 5;
	}

	void OnEnemyKilled() {
		if (Time.time < lastKill + streakExpiry) {
			streakCount++;
		} else {
			streakCount = 0;
		}

		lastKill = Time.time;

		score += 10 + (int) Mathf.Pow(3,streakCount);
	}

	void OnPlayerDeath() {
		Enemy.OnDeathStatic -= OnEnemyKilled;
	}
}
