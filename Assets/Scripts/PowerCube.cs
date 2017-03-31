using UnityEngine;
using System.Collections;

public class PowerCube : MonoBehaviour {
	public ParticleSystem deathEffect;
	int powerups =13;
	int rnd;

	public static event System.Action<string> OnEnpowered;
	//timer reference here


	void OnTriggerEnter(Collider c) {
		if (c.CompareTag("Player")) {
			rnd = Random.Range (1, powerups);
			//rnd = Random.Range (1, powerups);
			LivingEntity obj = c.gameObject.GetComponent<LivingEntity> ();
			Player p = c.gameObject.GetComponent<Player> ();
			//debug
			AudioManager.instance.PlaySound ("PowerUp", p.transform.position);
			string power = "No Powerup";

			if (obj != null) {
				switch (rnd) {
				case 1: //mine
					obj.SetAmmo (1);
					power = "Mine";
					break;
				case 2: //shield
					obj.SetAmmo (2);
					power = "Shield";
					break;
				case 3: //slow mo explode all mines 
					obj.SetAmmo(3);
					power = "Mine Exploder";
					break;
				case 4: //booster
					p.SetBoosterActive ();
					power = "Booster";
					break;
				case 5: //health
					obj.AddHealth (3);
					power = "Health";
					//show +Health above player
					break;
				case 6: //brake lock
					p.SetBrakerActive ();
					power = "Brake Lock";
					break;
				case 7: //reverse steer
					p.SetReverseSteeringActive ();
					power = "Revesre Steer";
					break;
				case 8: //timer
					obj.SetAmmo (1);
					power = "Mine";
					break;
				case 9: //mine
					obj.SetAmmo (1);
					power = "Mine";
					break;
				case 10: //mine
					obj.SetAmmo (1);
					power = "Mine";
					break;
				case 11: //mine
					obj.SetAmmo (1);
					power = "Mine";
					break;
				case 12: //mine
					obj.SetAmmo (2);
					power = "Shield";
					break;
				}

				if (OnEnpowered != null) {
					OnEnpowered (power);
				}

			}

			GameObject deathParticles = Instantiate(deathEffect, transform.position, Quaternion.identity) as GameObject;
			Destroy (deathParticles, deathEffect.startLifetime ) ;
			GameObject.Destroy (gameObject);
		}

	}


}
