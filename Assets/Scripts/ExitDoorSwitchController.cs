using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorSwitchController : MonoBehaviour {

	private Animator myAnimator;
	private bool currentState;
	private bool axisInUse = false;
	public string responsableAxis = "Fire2";

	void Awake() {
		currentState = false;
	}
	// Use this for initialization
	void Start () {
		myAnimator = gameObject.GetComponent<Animator> ();	
	}
	
	public void setState(bool newState) {

		if (newState != currentState) {
			currentState = newState;
			myAnimator.SetBool ("state", currentState);
		}

	}

	public bool getState() {
		return currentState;
	}

	public void Update() {
		if (Input.GetAxis (responsableAxis) == 0.0f) {
			axisInUse = false;
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {

			if (Input.GetAxisRaw (responsableAxis) != 0.0f && axisInUse == false) {
				axisInUse = true;
				setState (!currentState);

				if (currentState) {
					FXAudio.playClip ("SwitchGreen", 0.5f);
				} else {
					FXAudio.playClip ("SwitchRed", 0.5f);
				}
			}

		}
	}
}
