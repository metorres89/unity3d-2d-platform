using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableGemController : MonoBehaviour {

	public int value = 1000;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player" && PlayerState.HealthPoints > 0.0f) {
			PlayerState.Score += value;
			FXAudio.PlayClip ("PickupCoin", 0.5f);
			Destroy (gameObject);
		}
	}
}
