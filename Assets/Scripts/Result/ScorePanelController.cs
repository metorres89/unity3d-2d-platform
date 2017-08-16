using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanelController : MonoBehaviour {

	public Text gemScoreText;
	public Text killScoreText;
	public Text timeScoreText;
	public Text totalScoreText;

	void Start () {
		updateText ();
	}

	private void updateText() {
		gemScoreText.text = GameState.GetGemScore ().ToString();
		killScoreText.text = GameState.GetKillScore ().ToString();
		timeScoreText.text = string.Format("minutes: {0} seconds: {1}", GameState.GetTimeMinuteScore (), GameState.GetTimeSecondScore());
		totalScoreText.text = GameState.GetTotalScore ().ToString ();
	}
}
