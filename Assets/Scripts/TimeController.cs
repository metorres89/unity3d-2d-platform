using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

	public float totalTime = 240.0f;

	//private float myChronometer;

	// Use this for initialization
	void Start () {
		PlayerState.remainingTime = totalTime;
	}
	
	// Update is called once per frame
	void Update () {

		if(PlayerState.remainingTime > 0.0f)
			PlayerState.remainingTime -= Time.deltaTime;

		if (PlayerState.remainingTime <= 0.0f) {
			TimeOut ();
		}

	}

	void TimeOut() {
		GameState.setState(GameState.ResultType.GAME_OVER);
	}
}
