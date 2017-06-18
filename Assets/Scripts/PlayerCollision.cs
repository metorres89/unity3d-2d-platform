using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "ground") {

			if (col.contacts [0].normal.x == 0 && col.contacts [0].normal.y == 1) {
				//isOnGround false;	
				/*PlayerState.isOnGround = true;

				Debug.LogFormat("Player has landed on platform! {0}", col.gameObject.name);*/
			}
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag == "ground") {
			
			if (col.contacts [0].normal.x == 0 && col.contacts [0].normal.y == 1) {
				//isOnGround true;
				/*PlayerState.isOnGround = false;
				Debug.LogFormat ("Player has jumped or falled platform! {0}", col.gameObject.name);*/

			}
		}
	}
}
