using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrampController : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col){

		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<PlayerController> ().ReceiveDamage (1.0f);
		} else if (col.gameObject.tag == "Enemy") {
			col.gameObject.GetComponent<ZombieController> ().ReceiveDamage (1.0f);
		}
	}
}
