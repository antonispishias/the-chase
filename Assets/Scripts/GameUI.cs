using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

	public Image fadeBackground;
	public GameObject gameOverUI;
	public GameObject pauseUI;
	public Text powerUI;
	public Text powerTimerUI;
	public Text scoreUI;
	public Text timerUI;
	public Text waveUI;
	public Text popupUI;
	public RectTransform healthBar;
	float endTime, liveDuration =1f;

	GameTimer simpleTimer;
	Player player;

	void Start () {
		FindObjectOfType<Spawner> ().OnNewWave += DisplayNewWave;
		simpleTimer = GetComponent<GameTimer> ();
		StartCoroutine (Fade (Color.black , Color.clear, 3));
		player = FindObjectOfType<Player> ();
		player.OnDeath += OnGameOver;
		PowerCube.OnEnpowered += OnPowerUp;
		player.OnDeath += OnPlayerDeath;
		powerUI.text = powerTimerUI.text = timerUI.text = scoreUI.text = waveUI.text= popupUI.text = "";
		player.OnShoot += ClearPowerUp; //onShot
		simpleTimer.OnTime += ClearPowerUp;// on timer expired
	}
		
	void Update() {
		if (Input.GetKeyDown(KeyCode.P)) {
			Pause ();
		}

		scoreUI.text = ScoreKeeper.score.ToString("D6");
		float healthPercent = 0;
		if (player != null) {
			 healthPercent = player.health / player.startingHealth;
		}

		healthBar.localScale = new Vector3 (healthPercent, 1, 1);

		if (simpleTimer.time > 0) {
			powerTimerUI.text = simpleTimer.time.ToString ("0.00");
		} else {
			powerTimerUI.text = "";
		}

		timerUI.text = simpleTimer.MainTimeDiplay();

		if (Time.time > endTime) {
			popupUI.text = "";
		}
	}

	void OnPlayerDeath() {
		PowerCube.OnEnpowered -= OnPowerUp;
	}
		
	void OnGameOver () {
		StartCoroutine (Fade (Color.clear, Color.black, 3));
		gameOverUI.SetActive( true);  
	}

	public void Pause () {
		if (Time.timeScale == 1.0F) {
			Time.timeScale = 0.0F;
			ShowPauseMenu ();
		} else {
			Time.timeScale = 1.0F;
			HidePauseMenu ();
		}
		Time.fixedDeltaTime = 0.02F * Time.timeScale;
	}

	void ShowPauseMenu() {
		pauseUI.SetActive( true); 
	}

	void HidePauseMenu() {
		pauseUI.SetActive( false); 
	}


	IEnumerator Fade (Color from, Color to, float time) {
		float speed = 1 / time;
		float p = 1;

		while (p <= 1) {
			p += Time.deltaTime * speed;
			fadeBackground.color = Color.Lerp (from, to, speed);
			yield return null;
		}

	}

	public void DisplayNewWave(int w, int e) {
		waveUI.text = string.Format ("Wave: {0:00} \nEnemies: {1:000}", w, e);
		int extraBonus = 0;
		AudioManager.instance.PlaySound ("New Wave", transform.position);
		if (w > 1) {
			SetDuration ();
			extraBonus = 50 * w;
		}
		popupUI.text = "+"+extraBonus.ToString ();
	}

	public void ClearPowerUp () {
		powerUI.text = "";
		simpleTimer.Zero ();
	}


	public void OnPowerUp (string power) {
		powerUI.text = power;
		if (power == "Health") {
			SetDuration ();
			popupUI.text = "+Health";
			simpleTimer.Zero ();
		} else {
			simpleTimer.ResetTimer();
		}
	}


	public void ReturnToMenu () {
		SceneManager.LoadScene ("Menu");
	}

	public void StartNewGame() {
		SceneManager.LoadScene  (Application.loadedLevelName);
	}

	void SetDuration() {
		endTime = Time.time + liveDuration;
	}
}
