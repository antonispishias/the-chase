using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour {

	public Transform muzzleFront;
	public Transform muzzleBack;
	public Projectile bullet;
	public Minefield mine;
	public Shield shield;
	public float fireRateLazer = 100;
	public float muzzleVel = 35;

	List<Minefield> allMines;

	float nextShotTime;

	void Start() {
		allMines = new List<Minefield> ();
	}

	public void Shoot (int projectile) {

		if (Time.time > nextShotTime) {
			
			switch (projectile) {
			case 0:
				nextShotTime = Time.time + fireRateLazer/1000;
				Projectile newBullet = Instantiate (bullet, muzzleFront.position, muzzleFront.rotation) as Projectile;
				newBullet.SetSpeed (muzzleVel);
				break;
			case 1:
				Minefield newMine = Instantiate (mine, muzzleBack.position, muzzleBack.rotation) as Minefield;
				allMines.Add (newMine);
				break;
			case 2:
				Instantiate (shield, muzzleFront.position, muzzleFront.rotation);
				break;
			case 3:
				foreach (Minefield m in allMines) {
					if (m != null) {
						m.MineExplosion ();
					} 
				}
				var allDropCubes = FindObjectsOfType<DropCube> ();
				foreach (DropCube d in allDropCubes) {
					if (d != null) {
						d.CubeExplosion ();
					}
				}
				break;
			}



		}
	}
}
