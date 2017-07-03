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
		if (Input.GetAxis (responsableAxis) == 0.0f) {
			axisInUse = false;
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {

			if (Input.GetAxisRaw (responsableAxis) != 0.0f && axisInUse == false) {
				axisInUse = true;
				SetState (!currentState);

				if (currentState) {
					FXAudio.PlayClip ("SwitchGreen");
				} else {
					FXAudio.PlayClip ("SwitchRed");
				}
			}

		}
	}
}
