using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorController : MonoBehaviour {

	private Animator myAnimator;
	private DoorState currentState;
	private bool axisInUse = false;
	private GameObject[] allSwitches;
	public float delayOpenDoor = 1.0f;

	public enum DoorState
	{
		LOCKED = 0,
		UNLOCKED = 1,
		OPEN = 2
	};

	public string switchTag = "Switch";

	void Awake() {
		currentState = DoorState.LOCKED;
		axisInUse = false;
	}

	void Start () {
		myAnimator = gameObject.GetComponent<Animator> ();
		allSwitches = GameObject.FindGameObjectsWithTag (switchTag);

		PlayerState.TotalDoorSwitches = allSwitches.Length;
	}

	void Update() {
		if (Input.GetAxisRaw ("Fire2") == 0.0f) {
			axisInUse = false;
		}

		if (currentState == DoorState.UNLOCKED) {

			delayOpenDoor -= Time.deltaTime;

			if (delayOpenDoor <= 0.0f) {

				SetState (DoorState.OPEN);

			}
		}
	}

	private void SetState(DoorState newState){
		if (newState != currentState) {
			currentState = newState;

			myAnimator.SetInteger ("state", (int)currentState);
		}
	}

	public DoorState GetState() {
		return currentState;
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			
			if (Input.GetAxis ("Fire2") != 0.0f && axisInUse == false) {

				axisInUse = true;

				if (currentState == DoorState.LOCKED) {
					bool tryToUnlock = TryToUnlock ();

					Debug.LogFormat ("Player is trying to unlock the door , the result: {0}", tryToUnlock);

					if (tryToUnlock) {
						SetState (DoorState.UNLOCKED);

						FXAudio.PlayClip ("DoorUnlocked");
				
					} else {
					
						FXAudio.PlayClip ("DoorLocked");
					}
				} else if (currentState == DoorState.OPEN) {
					
					GameState.SetState (GameState.ResultType.WIN, "Result");
				
				}
			}
		}
	}

	public bool TryToUnlock () {
		
		int greenSwitches = 0;

		foreach (GameObject goSwitch in allSwitches) {

			ExitDoorSwitchController edsc = goSwitch.GetComponent<ExitDoorSwitchController> ();
			if (edsc.GetState ())
				greenSwitches++;
		}

		return greenSwitches == allSwitches.Length;
	}
}
