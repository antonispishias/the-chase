using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashDelay : MonoBehaviour {

	public float delay = 3f;

	IEnumerator Start () {
		yield return new WaitForSeconds (delay);
		SceneManager.LoadScene ("Menu");
	}

}
