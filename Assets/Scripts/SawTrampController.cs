using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrampController : MonoBehaviour {

	public float damage = 1.0f;

	void OnCollisionEnter2D(Collision2D col){

		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<PlayerController> ().ReceiveDamage (damage);
		} else if (col.gameObject.tag == "Enemy") {
			col.gameObject.GetComponent<ZombieController> ().ReceiveDamage (damage);
		}

		//SOUND FX - SAW
	}
}
