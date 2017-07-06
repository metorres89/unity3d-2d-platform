using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrampController : MonoBehaviour {

	public float damage = 1.0f;
	public float impactForce = 200.0f;

	void OnCollisionEnter2D(Collision2D col){

		if (col.gameObject.tag == "Player") {
			PlayerController pc = col.gameObject.GetComponent<PlayerController> ();
			pc.ReceiveDamage (damage);
			pc.ReceiveImpact (col.contacts[0].point * impactForce);
		} else if (col.gameObject.tag == "Enemy") {
			col.gameObject.GetComponent<ZombieController> ().ReceiveDamage (damage);

		}

		//SOUND FX - SAW
	}
}
