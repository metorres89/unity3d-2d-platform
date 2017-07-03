using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

	public float totalTime = 240.0f;

	//private float myChronometer;

	// Use this for initialization
	void Start () {
		PlayerState.RemainingTime = totalTime;
	}
	
	// Update is called once per frame
	void Update () {

		if(PlayerState.RemainingTime > 0.0f)
			PlayerState.RemainingTime -= Time.deltaTime;

		if (PlayerState.RemainingTime <= 0.0f) {
			TimeOut ();
		}

	}

	void TimeOut() {
		GameState.SetState(GameState.ResultType.GAME_OVER, "Result");
	}
}
