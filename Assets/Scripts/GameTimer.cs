using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

	public float mainTime { get; protected set;}
	public float time {get; protected set;}
	public event System.Action OnTime;
	Player player;

	void Start() {
		player = FindObjectOfType<Player> ();
		Zero ();
		MainReset ();
	}

	public void MainReset () {
		mainTime = 0f;	
	}

	public void Zero () {
		time=0;
	}

	public void ResetTimer(float t=4) {
		time = t;
	}

	void Update () {
		if (player != null) {
			mainTime += Time.deltaTime;
		}

		time -= Time.deltaTime;

		if (time <= 0) {
			if (OnTime != null) {
				OnTime ();
			} 
		}
	}

	public string MainTimeDiplay() {
		System.TimeSpan t = System.TimeSpan.FromSeconds(mainTime);
		string timerFormatted = string.Format("{0:D2}:{1:D2}.{2:D3}", t.Minutes, t.Seconds, t.Milliseconds);
		return timerFormatted;
	}

}
