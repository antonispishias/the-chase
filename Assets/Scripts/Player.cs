using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerController))]
[RequireComponent (typeof (GunController))]

public class Player : LivingEntity {

	public float wheelRotation = 1f;
	public float moveSpeed = 1f;
	public float turnSpeed = 1f;
	PlayerController controller;
	GunController gunController;
	public Transform[] frontWheels;
	Vector3 fws = new Vector3(0,0,90);
	public event System.Action OnShoot;

	bool booster = false, braker= false, rev = false;
	float powerDuration = 4, powerOff;

	protected override void Start () {
		base.Start ();
		controller = GetComponent<PlayerController> ();
		gunController = GetComponent<GunController> ();
		FindObjectOfType<GameTimer> ().OnTime += PowerUpTimeout;
	}

	void Update () {
		//movement input
		float moveInput = Input.GetAxisRaw ("Vertical");
		float moveVelocity = moveInput * moveSpeed;
		if (booster) {
			if (Time.time > powerOff) {
				booster = false;
			}
			controller.Move (moveVelocity * 4);
			//start timer
		} else if (braker) {
			if (Time.time > powerOff) {
				braker = false;
			}
			controller.Move (0);
		} else {
			controller.Move (moveVelocity);
		}


		float turnInput = Input.GetAxisRaw ("Horizontal");
		if (rev) {
			if (Time.time > powerOff) {
				rev = false;
			}
			turnInput = -turnInput;
		}
		float turnVelocity = turnInput * turnSpeed;
		controller.Turn (turnVelocity*Input.GetAxisRaw("Vertical"));

		if (turnInput != 0) {
			frontWheels [0].localEulerAngles = 
				frontWheels [1].localEulerAngles = new Vector3 (fws.x, (fws.y + turnVelocity) * wheelRotation, fws.z);
		} else {
			frontWheels [0].localEulerAngles = 
				frontWheels[1].localEulerAngles = fws;
		}

		//weapon input
		if (Input.GetAxis("Fire1") > 0) {
			gunController.Shoot(0);
		}

		if (Input.GetAxis("Fire2") > 0 && ammo>0) {
			gunController.Shoot(ammo);
			if (OnShoot != null) {
				OnShoot ();
			}
			SetAmmo (0);
		}
			

	}

	void PowerUpTimeout () {
		SetAmmo (0);
	}



	public void SetBoosterActive() {
		booster = true;
		SetDuration ();
	}

	public void SetBrakerActive() {
		braker = true;
		SetDuration ();
	}

	public void SetReverseSteeringActive() {
		rev = true;
		SetDuration ();
	}

	void SetDuration() {
		powerOff = Time.time + powerDuration;
	}

}
