using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour , IDamageable {

	public float startingHealth;
	public float health { get; protected set;}
	protected bool dead;
	protected int ammo;
	protected float ammoOff;
	protected float ammoTime = 5;
	public event System.Action OnDeath;

	protected virtual void Start () {
		health = startingHealth;
	}

	public virtual void TakeHit (float damage, Vector3 hitPoint, Vector3 hitDirection) {
		TakeDamage (damage);
	}

	public virtual void TakeDamage (float damage) {
		health -= damage;
		if (health <= 0 && !dead) {
			Die();
		}
	}

	public void SetAmmo (int _ammo) {
		ammoOff = Time.time + ammoTime;
		// start a timer on screen
		this.ammo = _ammo;
		//Debug.Log (ammo);
	}

	public void AddHealth (float _health) {
		health += _health;
		AudioManager.instance.PlaySound ("Health", transform.position);
	}

	protected void Die() {
		dead = true;
		if (OnDeath != null) {
			OnDeath();
		}
		AudioManager.instance.PlaySound ("Death", transform.position);
		GameObject.Destroy (gameObject);
	}
		
}
