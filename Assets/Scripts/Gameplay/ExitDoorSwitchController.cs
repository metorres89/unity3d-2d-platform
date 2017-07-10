using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorSwitchController : MonoBehaviour {

	private Animator myAnimator;
	private bool currentState;
	private bool axisInUse = false;

	void Awake() {
		currentState = false;
	}
	// Use this for initialization
	void Start () {
		myAnimator = gameObject.GetComponent<Animator> ();	
	}
	
	public void SetState(bool newState) {

		if (newState != currentState) {
			currentState = newState;
			myAnimator.SetBool ("state", currentState);
		}

	}

	public bool GetState() {
		return currentState;
	}

	public void Update() {
		if (Input.GetAxis ("Fire2") == 0.0f) {
			axisInUse = false;
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {

			if (Input.GetAxisRaw ("Fire2") != 0.0f && axisInUse == false) {
				axisInUse = true;
				SetState (!currentState);

				if (currentState) {
					FXAudio.PlayClip ("SwitchGreen");
					PlayerState.ActivatedDoorSwitches++;
				} else {
					FXAudio.PlayClip ("SwitchRed");
					PlayerState.ActivatedDoorSwitches--;
				}
			}

		}
	}
}
