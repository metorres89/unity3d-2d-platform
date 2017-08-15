using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour {

	public bool instantKill = true;

	public float damage;

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log (col.gameObject.tag);

		float currentDamage = 0.0f;

		if (col.gameObject.tag == "Player") {

			if (instantKill)
				currentDamage = PlayerState.HealthPoints;
			else
				currentDamage = damage;

			col.gameObject.GetComponent<PlayerController> ().ReceiveDamage (currentDamage);

		} else if (col.gameObject.tag == "Enemy") {

			ZombieController zc = col.gameObject.GetComponent<ZombieController> ();

			if (instantKill)
				currentDamage = zc.healthPoints;
			else
				currentDamage = damage;

			zc.ReceiveDamage (currentDamage);
		}
	}
}
