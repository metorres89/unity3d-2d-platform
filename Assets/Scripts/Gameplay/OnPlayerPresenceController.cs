using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerPresenceController : MonoBehaviour {
	private ZombieController myZombieController;

	// Use this for initialization
	void Start () {
		myZombieController = gameObject.transform.parent.gameObject.GetComponent<ZombieController> ();	
	}

	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log (col.gameObject.tag);

		if (myZombieController.healthPoints > 0.0f) {

			if (col.gameObject.tag == "Player") {
				//PlayerController pc = col.gameObject.GetComponent<PlayerController> ();

				float distanceInX = gameObject.transform.position.x - col.gameObject.transform.position.x;

				if ( (distanceInX > 0.0f && myZombieController.IsFlipOnX() == false) || (distanceInX < 0.0f && myZombieController.IsFlipOnX() == true) ) {
					myZombieController.Flip ();
				}

			}
		}
	}
}
