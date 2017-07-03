using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerPresenceController : MonoBehaviour {
	private ZombieController myZombieController;
	public float speedUp = 2.0f;

	void Start () {
		myZombieController = gameObject.transform.parent.gameObject.GetComponent<ZombieController> ();	
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (myZombieController.healthPoints > 0.0f) {

			if (col.gameObject.tag == "Player") {
				float distanceInX = gameObject.transform.position.x - col.gameObject.transform.position.x;

				if ( (distanceInX > 0.0f && myZombieController.IsFlipOnX() == false) || (distanceInX < 0.0f && myZombieController.IsFlipOnX() == true) ) {
					myZombieController.Flip ();
				}

				myZombieController.walkingSpeed *= speedUp;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (myZombieController.healthPoints > 0.0f) {
			if(col.gameObject.tag == "Player") {
				myZombieController.walkingSpeed /= speedUp;
			}
		}
	}
}
