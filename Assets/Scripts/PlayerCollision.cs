using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy" && col.contacts[0].normal.y > 0) {

			col.gameObject.GetComponent<ZombieController> ().ReceiveDamage (1.0f);

		}
	}
}
