using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class DropCube : LivingEntity {


	public ParticleSystem deathEffect;
	public static event System.Action OnDeathStatic;

	protected override void Start () {
		base.Start();
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

	public void CubeExplosion() {
		TakeHit (3, transform.position, Vector3.up);
	}
}
