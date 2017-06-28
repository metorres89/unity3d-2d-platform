using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableGemController : MonoBehaviour {

	public int value = 1000;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			PlayerState.score += value;

			FXAudio.playClip ("PickupCoin", 0.5f);

			Destroy (gameObject);
		}
	}
}
