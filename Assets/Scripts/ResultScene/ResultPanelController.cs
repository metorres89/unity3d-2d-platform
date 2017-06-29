using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanelController : MonoBehaviour {
	public Text titleText;

	// Use this for initialization
	void Start () {
		if (titleText == null) {
			Transform t = gameObject.transform.Find ("TitleText");
			if (t != null)
				titleText = t.gameObject.GetComponent<Text> ();
		}

		updateText ();
	}

	private void updateText() {
		if (titleText != null) {
			if (GameState.getCurrentState() == GameState.ResultType.GAME_OVER) {
				titleText.text = "Game Over ):";
			} else if (GameState.getCurrentState() == GameState.ResultType.WIN) {
				titleText.text = "You WIN :)";
			}
		}
	}
}
