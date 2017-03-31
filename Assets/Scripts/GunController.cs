using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

	public Transform weaponHold;
	public Gun mainGun;
	Gun equippedGun;

	void Start (){
		if (mainGun != null) {
			EquipGun(mainGun);
		}
	}

	public void EquipGun (Gun g) {
		if (equippedGun != null) {
			Destroy(equippedGun.gameObject);
		}
		equippedGun = Instantiate (g, weaponHold.position, weaponHold.rotation) as Gun;
		equippedGun.transform.parent = weaponHold;
	}

	public void Shoot (int a) {
		if (equippedGun != null) {
			equippedGun.Shoot(a);
		}
	}
}
