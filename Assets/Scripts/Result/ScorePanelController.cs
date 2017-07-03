using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanelController : MonoBehaviour {
	public Text gemScoreText;
	public Text killScoreText;
	public Text timeScoreText;
	public Text totalScoreText;

	// Use this for initialization
	void Start () {
		if (gemScoreText == null) {
			Transform t = gameObject.transform.Find ("GemScoreLabel");
			if (t != null)
				gemScoreText = t.gameObject.GetComponent<Text> ();
		}

		if (killScoreText == null) {
			Transform t = gameObject.transform.Find ("KillScoreLabel");
			if (t != null)
				killScoreText = t.gameObject.GetComponent<Text> ();
		}

		if (timeScoreText == null) {
			Transform t = gameObject.transform.Find ("TimeScoreLabel");
			if (t != null)
				timeScoreText = t.gameObject.GetComponent<Text> ();
		}

		if (totalScoreText == null) {
			Transform t = gameObject.transform.Find ("TotalScoreLabel");
			if (t != null)
				totalScoreText = t.gameObject.GetComponent<Text> ();
		}

		updateText ();

	}

	private void updateText() {
		if (gemScoreText != null) {
			gemScoreText.text = GameState.GetGemScore ().ToString();
		}

		if (killScoreText != null) {
			killScoreText.text = GameState.GetKillScore ().ToString();
		}

		if (timeScoreText != null) {
			timeScoreText.text = string.Format("score x minutes: {0} \n score x seconds: {1}", GameState.GetTimeMinuteScore (), GameState.GetTimeSecondScore());
		}

		if (totalScoreText != null) {
			totalScoreText.text = GameState.GetTotalScore ().ToString ();
		}
	}
}
