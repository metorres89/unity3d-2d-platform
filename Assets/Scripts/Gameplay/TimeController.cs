using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour {

	public float totalTime = 240.0f;

	void Start () {
		PlayerState.RemainingTime = totalTime;
	}

	void Update () {
		if (Time.timeScale > 0.0f) {
			if (PlayerState.RemainingTime > 0.0f)
				PlayerState.RemainingTime -= Time.deltaTime;

			if (PlayerState.RemainingTime <= 0.0f) {
				TimeOut ();
			}
		}
	}

	void TimeOut() {
		GameState.SetState(GameState.ResultType.GAME_OVER);
		SceneManager.LoadScene ("Result", LoadSceneMode.Single);
	}
}
