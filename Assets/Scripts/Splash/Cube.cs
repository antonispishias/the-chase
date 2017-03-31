using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

	public ParticleSystem death;
	Rigidbody rb;

	void Start () {
			rb = GetComponent<Rigidbody>();
			if (rb!=null) {
				rb.AddTorque(new Vector3(0,480,0));
			}
	}

	void OnTriggerEnter(Collider other) {
		Explode ();
	}

	public void Explode() {
		Destroy (gameObject);
		Destroy (Instantiate (death, transform.position, Quaternion.Euler (new Vector3 (-90,0,0))), 5f);
	}
}
