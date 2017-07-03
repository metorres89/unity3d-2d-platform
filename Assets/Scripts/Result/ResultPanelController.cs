using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultPanelController : MonoBehaviour {
	public Text titleText;
	public Button restartButton;
	public Button mainMenuButton;

	// Use this for initialization
	void Start () {
		if (titleText == null) {
			Transform t = gameObject.transform.Find ("TitleText");
			if (t != null)
				titleText = t.gameObject.GetComponent<Text> ();
		}

		if (restartButton == null) {
			Transform t = gameObject.transform.Find ("RestartButton");
			if (t != null)
				restartButton = t.gameObject.GetComponent<Button> ();
		}

		if (mainMenuButton == null) {
			Transform t = gameObject.transform.Find ("MainMenuButton");
			if (t != null)
				mainMenuButton = t.gameObject.GetComponent<Button> ();
		}

		updateText ();

		if(restartButton != null)
			restartButton.onClick.AddListener (restart);

		if(mainMenuButton != null)
			mainMenuButton.onClick.AddListener (mainMenu);
	}

	private void updateText() {
		if (titleText != null) {
			if (GameState.GetCurrentState() == GameState.ResultType.GAME_OVER) {
				titleText.text = "Game Over ):";
			} else if (GameState.GetCurrentState() == GameState.ResultType.WIN) {
				titleText.text = "You WIN :)";
			}
		}
	}

	private void restart(){
		Debug.Log ("restarting game!!!");

		PlayerState.Reset ();
		GameState.SetState (GameState.ResultType.INITIAL, "Gameplay");
	}

	private void mainMenu(){
		PlayerState.Reset ();
		GameState.SetState (GameState.ResultType.INITIAL, "MainMenu");
	}
}
