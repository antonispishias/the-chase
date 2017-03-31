using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class RandomRotator : MonoBehaviour {

	public float tumble;

	void Start() {
		GetComponent<Rigidbody> ().angularVelocity = Random.insideUnitSphere * tumble;

	}
}
