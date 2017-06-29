using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorController : MonoBehaviour {

	private Animator myAnimator;
	private DoorState currentState;
	private bool axisInUse = false;

	public float delayOpenDoor = 1.0f;

	public enum DoorState
	{
		LOCKED = 0,
		UNLOCKED = 1,
		OPEN = 2
	};

	public string switchTag = "Switch";
	public string responsableAxis = "Fire2";

	void Awake() {
		currentState = DoorState.LOCKED;
		axisInUse = false;
	}

	void Start () {
		myAnimator = gameObject.GetComponent<Animator> ();
	}

	void Update() {
		if (Input.GetAxisRaw (responsableAxis) == 0.0f) {
			axisInUse = false;
		}

		if (currentState == DoorState.UNLOCKED) {

			delayOpenDoor -= Time.deltaTime;

			if (delayOpenDoor <= 0.0f) {

				setState (DoorState.OPEN);

			}
		}
	}

	private void setState(DoorState newState){
		if (newState != currentState) {
			currentState = newState;

			myAnimator.SetInteger ("state", (int)currentState);
		}
	}

	public DoorState getState() {
		return currentState;
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			
			if (Input.GetAxis ("Fire2") != 0.0f && axisInUse == false && currentState == DoorState.LOCKED) {

				axisInUse = true;

				bool tryToUnlock = TryToUnlock ();

				Debug.LogFormat ("Player is trying to unlock the door , the result: {0}", tryToUnlock);

				if (tryToUnlock) {
					setState (DoorState.UNLOCKED);

					FXAudio.playClip ("DoorUnlocked", 0.5f);
				
				} else {
					
					FXAudio.playClip ("DoorLocked", 0.5f);
				}
			}
		}
	}

	public bool TryToUnlock () {
		GameObject[] allSwitches = GameObject.FindGameObjectsWithTag (switchTag);

		int greenSwitches = 0;

		foreach (GameObject goSwitch in allSwitches) {

			ExitDoorSwitchController edsc = goSwitch.GetComponent<ExitDoorSwitchController> ();
			if (edsc.getState ())
				greenSwitches++;
		}

		return greenSwitches == allSwitches.Length;
	}
}
