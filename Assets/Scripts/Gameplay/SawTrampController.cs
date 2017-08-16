using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrampController : MonoBehaviour {
	private Rigidbody2D myRigidBody;
	public float damage = 1.0f;

	void Start()
	{
		myRigidBody = gameObject.GetComponent<Rigidbody2D> ();

		myRigidBody.angularVelocity = 360.0f;
	}

	void OnCollisionEnter2D(Collision2D col){

		if (col.gameObject.tag == "Player") {
			PlayerController pc = col.gameObject.GetComponent<PlayerController> ();
			pc.ReceiveDamage (damage);
		} else if (col.gameObject.tag == "Enemy") {
			col.gameObject.GetComponent<ZombieController> ().ReceiveDamage (damage);

		}

		//SOUND FX - SAW
	}
}
