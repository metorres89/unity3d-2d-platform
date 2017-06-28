using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrampController : MonoBehaviour {

	public bool instantKill = true;
	public float damage;

	void OnCollisionEnter2D(Collision2D col){

		float currentDamage = 0.0f;

		if (col.gameObject.tag == "Player" && col.contacts [0].normal.y < 0) {

			if (instantKill)
				currentDamage = PlayerState.HP;
			else
				currentDamage = damage;
			
			col.gameObject.GetComponent<PlayerController> ().ReceiveDamage (currentDamage);

		} else if (col.gameObject.tag == "Enemy" && col.contacts [0].normal.y < 0) {

			ZombieController zc = col.gameObject.GetComponent<ZombieController> ();

			if (instantKill)
				currentDamage = zc.HP;
			else
				currentDamage = damage;

			zc.ReceiveDamage (damage);
		}
	}
}
