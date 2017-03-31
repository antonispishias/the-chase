using UnityEngine;
using System.Collections;


[RequireComponent (typeof (EnemyController))]
[RequireComponent (typeof (NavMeshAgent))]
public class Enemy : LivingEntity {

	public enum State {Idle,Chasing,Attacking};
	public static event System.Action OnDeathStatic;

	public ParticleSystem deathEffect;

	State currentState;
	LivingEntity targetEntity;
	NavMeshAgent pathfinder;
	Transform target;
	Material skinMaterial;
	Color originalColor;
//	Rigidbody rb;

	float attackDistance = .5f; 
	float timeBetweenAttacks = 2f;
	float attackBounds = 1.5f;
	float damage = 1f;

	float nextAttackTime;

	bool hasTarget;

	protected override void Start () {
		base.Start();
		
//		rb = GetComponent<Rigidbody> ();
		pathfinder = GetComponent<NavMeshAgent> ();
		skinMaterial = GetComponent<Renderer> ().material;
		originalColor = skinMaterial.color;

		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			currentState = State.Chasing;
			hasTarget = true;

			target = GameObject.FindGameObjectWithTag ("Player").transform;
			targetEntity = target.GetComponent<LivingEntity> ();
			targetEntity.OnDeath += OnTargetDeath;

			StartCoroutine (UpdatePath ());
		} else {
			
		}
	}
		
	void OnTargetDeath(){
		hasTarget = false;
		currentState = State.Idle;
	}

	public override void TakeHit (float damage, Vector3 hitPoint, Vector3 hitDirection) {
		if (damage >= health) {
			if (OnDeathStatic != null) {
				OnDeathStatic ();
			}
			GameObject deathParticles = Instantiate(deathEffect, hitPoint, Quaternion.FromToRotation(Vector3.forward,hitDirection)) as GameObject;
			Destroy (deathParticles, deathEffect.startLifetime ) ;
		}
		base.TakeHit (damage, hitPoint, hitDirection);
	}

	void Update () {
		
		if (hasTarget) {
			if (Time.time > nextAttackTime) {
				float sqrDistanceToTarget = (target.position - transform.position).sqrMagnitude;
				if (sqrDistanceToTarget < Mathf.Pow (attackDistance + attackBounds, 2)) {
					nextAttackTime = Time.time + timeBetweenAttacks;
					StartCoroutine (Attack ());
				}
			}
		}
	}

	IEnumerator Attack () {

		currentState = State.Attacking;
		pathfinder.enabled = false;

		
		skinMaterial.color = Color.red;

		Vector3 originalPosition = transform.position;
		Vector3 attackPosition = target.position;

		float attackSpeed = 3;
		float percent = 0;
		bool hasAppliedDamage = false;

		while (percent<=1) {

			if (percent>=.5 && !hasAppliedDamage) {
				hasAppliedDamage = true;
				targetEntity.TakeDamage(damage);
				AudioManager.instance.PlaySound ("Crash", transform.position);
			}

			percent += Time.deltaTime* attackSpeed;
			float interpolation = 4*(-Mathf.Pow(percent,2)+percent);
			transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
			yield return null;
		}
		skinMaterial.color = originalColor;

		currentState = State.Chasing;
		pathfinder.enabled = true;
	}

	IEnumerator UpdatePath () {
		float refreshRate = .25f;
		while (hasTarget) {
			if (currentState == State.Chasing) {
				Vector3 directionToTarget = (target.position - transform.position).normalized;
				Vector3 targetPosition = target.position - directionToTarget * attackBounds;
				if (!dead) {
					pathfinder.SetDestination(targetPosition);
				}
			}
			yield return new WaitForSeconds(refreshRate);
		}
	}
}