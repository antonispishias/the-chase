using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {

	public float maxSpeed;
	//float velocity;
	float torque;
	float acceleration;
	float div = 2;
	Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	
	public void Move (float _acceleration){
		if (_acceleration > 0) {
			if ((acceleration + _acceleration/div) < maxSpeed) {
				
				acceleration += _acceleration/div;
			}
		} else if ((_acceleration < 0)) {
			if (-(acceleration -_acceleration/div) < maxSpeed) {
				acceleration += _acceleration/div;
			}
		}
		else {
			acceleration =0;
		}
	}

	public void Turn (float _torque){
		torque = _torque;

	}

	void FixedUpdate () {
		rb.AddForce (transform.forward * acceleration, ForceMode.Impulse);
		rb.AddTorque(transform.up * torque, ForceMode.Impulse);
	}





}
