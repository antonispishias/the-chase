using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {

	public GameObject mainMenuHolder;
	public GameObject optionsMenuHolder;


	public Slider[] volumeSliders;

	void Start () {
		volumeSliders [0].value = AudioManager.instance.masterVol;
		volumeSliders [1].value = AudioManager.instance.musicVol;
		volumeSliders [2].value = AudioManager.instance.sfxVol;
		AudioManager.instance.PlayMusic (AudioManager.instance.menuTrack);
	}

	public void OptionsMenu() {
		mainMenuHolder.SetActive (false);
		optionsMenuHolder.SetActive (true);
	}
	public void MainMenu() {
		optionsMenuHolder.SetActive (false);
		mainMenuHolder.SetActive (true);
	}

	public void SetMasterVolume(float value) {
		AudioManager.instance.SetVolume (value, AudioManager.Channel.Master);
	}
	public void SetMusicVolume(float value) {
		AudioManager.instance.SetVolume (value, AudioManager.Channel.Music);
	}
	public void SetSfxVolume(float value) {
		AudioManager.instance.SetVolume (value, AudioManager.Channel.Sfx);
	}

	public void Level2() {
		SceneManager.LoadScene("Arena 2");
		AudioManager.instance.PlayMusic (AudioManager.instance.mainTrack);
	}
	public void Level1() {
		SceneManager.LoadScene("Chase");
		AudioManager.instance.PlayMusic (AudioManager.instance.mainTrack);
	}
	public void Quit() {
		Application.Quit ();
	}
}
