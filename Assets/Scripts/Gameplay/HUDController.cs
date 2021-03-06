﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {
	
	public Text lifeText;
	public Text scoreText;
	public Text killsText;
	public Text timeText;
	public Text switchesText;

	// Use this for initialization
	void Start () {

		if (lifeText == null) {
			Transform t = gameObject.transform.Find ("LifeText");
			if (t != null)
				lifeText = t.gameObject.GetComponent<Text> ();
		}

		if (scoreText == null) {
			Transform t = gameObject.transform.Find ("ScoreText");
			if (t != null)
				scoreText = t.gameObject.GetComponent<Text> ();
		}

		if (killsText == null) {
			Transform t = gameObject.transform.Find ("KillsText");
			if (t != null)
				killsText = t.gameObject.GetComponent<Text> ();
		}

		if (timeText == null) {
			Transform t = gameObject.transform.Find ("TimeText");
			if (t != null)
				timeText = t.gameObject.GetComponent<Text> ();
		}

		if (switchesText == null) {
			Transform t = gameObject.transform.Find ("SwitchesText");
			if (t != null)
				switchesText = t.gameObject.GetComponent<Text> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (lifeText != null)
			lifeText.text = PlayerState.HealthPoints.ToString ();

		if (scoreText != null)
			scoreText.text = PlayerState.Score.ToString ();

		if (killsText != null)
			killsText.text = PlayerState.KilledEnemies.ToString ();

		if (timeText != null) {
			timeText.text = string.Format ("{0}:{1}", ParseToMinute (PlayerState.RemainingTime).ToString().PadLeft(2, '0'), ParseToSecond (PlayerState.RemainingTime).ToString().PadLeft(2, '0'));
		}

		if (switchesText != null) {
			switchesText.text = string.Format ("{0} / {1}", PlayerState.ActivatedDoorSwitches, PlayerState.TotalDoorSwitches);
		}
	}

	private int ParseToMinute(float time){
		return ((int)time) / 60;
	}

	private int ParseToSecond(float time){
		return ((int)time) % 60;
	}
}
