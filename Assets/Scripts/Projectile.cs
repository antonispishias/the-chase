using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public LayerMask collisionMask;
	float speed = 15f;
	float damage = 1f;
	float lifetime = 4f;
	float collisionWidth = .1f;

	void Start() {
		Destroy (gameObject, lifetime);
		Collider[] initailCollisions = Physics.OverlapSphere (transform.position, .1f, collisionMask);
		if (initailCollisions.Length>0) {
			OnHitObject(initailCollisions[0], transform.position);
		}
		AudioManager.instance.PlaySound ("Lazer", transform.position);
	}

	public void SetSpeed (float _speed) {
		speed = _speed;
	}

	void Update () {
		float moveDistance = speed * Time.deltaTime;
		CheckCollisions (moveDistance);
		transform.Translate (Vector3.forward * moveDistance);
	}

	void CheckCollisions (float moveDistance) {
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, moveDistance+collisionWidth, collisionMask, QueryTriggerInteraction.Collide)) {
			OnHitObject(hit.collider, hit.point);
		} 
	}
	

	void OnHitObject (Collider col, Vector3 hitPoint) {
		IDamageable damagableObject = col.GetComponent<IDamageable> ();
		if (damagableObject != null) {
			damagableObject.TakeHit(damage, hitPoint, transform.forward);
		}
		GameObject.Destroy (gameObject);
	}
}
