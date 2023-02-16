using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	public GameObject PurpleBullet;

	void ShootBullet() {
		if (Input.GetKeyDown (KeyCode.T)) {
			GameObject bullet = Instantiate (PurpleBullet, transform.position, Quaternion.identity);
			bullet.GetComponent<PurpleBullet> ().Speed *= transform.localScale.x;
		}
	}

	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ShootBullet ();
		
	}



} // class























