using UnityEngine;
using System.Collections;

public class Minefield : MonoBehaviour {

	public ParticleSystem mineExplosion;
	public LayerMask collisionMask;
//	private GameObject player;
	float damage = 5f;
	float lifetime = 20f;
	//float collisionWidth = .1f;
	
	
	void Start () {
		Destroy (gameObject, lifetime);
		Collider[] initailCollisions = Physics.OverlapSphere (transform.position, .1f, collisionMask);
		if (initailCollisions.Length>0) {
			OnHitObject(initailCollisions[0], transform.position);
		}
		//player = GameObject.FindGameObjectWithTag ("Player");
	}

	
	void OnTriggerEnter (Collider other) {
		IDamageable damagableObject = other.GetComponent<IDamageable> ();
		if (damagableObject != null) {
			damagableObject.TakeHit(damage, other.transform.position, -other.transform.forward);
			MineExplosion ();
		}
	}
	
	
	void OnHitObject (Collider col, Vector3 hitPoint) {
		IDamageable damagableObject = col.GetComponent<IDamageable> ();
		if (damagableObject != null) {
			damagableObject.TakeHit(damage, hitPoint, transform.forward);
			MineExplosion ();
		}
	}

	public void MineExplosion() {
		Collider[] initailCollisions = Physics.OverlapSphere (transform.position, 3f, collisionMask);
		if (initailCollisions.Length>0) {
			OnHitObject(initailCollisions[0], transform.position);
		}
		AudioManager.instance.PlaySound ("Explosion", transform.position);
		Destroy (Instantiate (mineExplosion, transform.position, transform.rotation), 3f );
		GameObject.Destroy(gameObject);
	}

}
