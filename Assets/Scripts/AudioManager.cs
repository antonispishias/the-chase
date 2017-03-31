using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public enum Channel{
		Master, Music, Sfx
	}

	public AudioClip mainTrack;
	public AudioClip menuTrack;


	public float masterVol { get; private set;}
	public float sfxVol { get; private set;}
	public float musicVol { get; private set;}
	AudioSource[] musicSources;
	int activeMusicSource;

	Transform audioListener;
	Transform player;

	SoundLibrary soundLib;

	public static AudioManager instance;

	void Awake() {
		if (instance != null) {
			Destroy (gameObject);
		} else {

			instance = this;
			DontDestroyOnLoad (this.gameObject);

			musicSources = new AudioSource[2];
			for (int i = 0; i < 2; i++) {
				GameObject newMusicSource = new GameObject ("Music Source " + (i + 1));
				musicSources [i] = newMusicSource.AddComponent<AudioSource> ();
				newMusicSource.transform.parent = transform;
			}


			soundLib = GetComponent<SoundLibrary> ();
			audioListener = FindObjectOfType<AudioListener> ().transform;
			if (FindObjectOfType<Player> () != null) {
				player = FindObjectOfType<Player> ().transform;
			}

			masterVol = PlayerPrefs.GetFloat ("master volume", 1);
			musicVol = PlayerPrefs.GetFloat ("music volume", 1);
			sfxVol = PlayerPrefs.GetFloat ("sfx volume", 1);
		}
	}

	void Start() {
		PlayMusic (menuTrack, 2);
	}

	void Update() {
		if (player == null) {
			if (FindObjectOfType<Player> () != null) {
				player = FindObjectOfType<Player> ().transform;
			}
		}

		if (player != null) {
			audioListener.position = player.position;
			audioListener.rotation = player.rotation;
		}
	}

	public void SetVolume (float volume, Channel c) {
		switch (c) {
		case Channel.Master:
			masterVol = volume;
			break;
		case Channel.Music:
			musicVol = volume;
			break;
		case Channel.Sfx:
			sfxVol = volume;
			break;
		}

		musicSources [0].volume = musicVol * masterVol;
		musicSources [1].volume = musicVol * masterVol;

		PlayerPrefs.SetFloat ("master volume", masterVol);
		PlayerPrefs.SetFloat ("music volume", musicVol);
		PlayerPrefs.SetFloat ("sfx volume", sfxVol);
		PlayerPrefs.Save ();
	}

	public void PlaySound(string name, Vector3 pos) {
		PlaySound(soundLib.GetClipFromName (name),pos);
	}

	public void PlaySound(AudioClip clip, Vector3 pos) {
		if (clip != null) {
			AudioSource.PlayClipAtPoint (clip, pos, sfxVol * masterVol);
		}
	}

	public void PlayMusic(AudioClip clip, float fadeDuration=1.2f) {
		if (clip != null) {
			activeMusicSource = 1 - activeMusicSource;
			musicSources [activeMusicSource].clip = clip;
			musicSources [activeMusicSource].volume = 1;
			musicSources [activeMusicSource].loop = true;
			musicSources [activeMusicSource].Play ();
			StartCoroutine (Transition (fadeDuration));
		}
	}

	IEnumerator Transition (float fadeDuration) {
		float percent = 0;
		while (percent < 1) {
			percent += Time.deltaTime * 1 / fadeDuration;
			musicSources [1-activeMusicSource].volume = Mathf.Lerp (musicVol * masterVol, 0, percent);
			musicSources [activeMusicSource].volume = Mathf.Lerp (0, musicVol * masterVol, percent);
			yield return null;
		}

	}


}
