using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	public LayerMask collisionMask;
	private GameObject player;
	float damage = 1f;
	float lifetime = 5f;
	float power = 5f;
	//float collisionWidth = .1f;


	void Start () {
		Destroy (gameObject, lifetime);
		Collider[] initailCollisions = Physics.OverlapSphere (transform.position, .1f, collisionMask);
		if (initailCollisions.Length>0) {
			OnHitObject(initailCollisions[0], transform.position);
		}
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update () {
		if (player != null && power > 0) {
			transform.position = player.transform.position;
		} else {
			GameObject.Destroy(gameObject);
		}
		//CheckCollisions ();
	}
	
	void OnTriggerEnter (Collider other) {

		if (other.gameObject.tag == "Enemy") {
			IDamageable damagableObject = other.GetComponent<IDamageable> ();
			if (damagableObject != null) {
				damagableObject.TakeHit(damage, other.transform.position, -other.transform.forward);
				power--;
			}
		}
	}
	
	
	void OnHitObject (Collider col, Vector3 hitPoint) {
		IDamageable damagableObject = col.GetComponent<IDamageable> ();
		if (damagableObject != null) {
			damagableObject.TakeHit(damage, hitPoint, transform.forward);
		}
		//GameObject.Destroy (gameObject);
	}
}
