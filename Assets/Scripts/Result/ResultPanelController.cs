using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultPanelController : MonoBehaviour {
	public Text titleText;
	public Button restartButton;
	public Button mainMenuButton;
	public Button quitButton;
	public Button nextButton;

	void Start () {
		MusicAudio.Init ();

		if (titleText == null) {
			Transform t = gameObject.transform.Find ("TitleText");
			if (t != null)
				titleText = t.gameObject.GetComponent<Text> ();
		}

		restartButton.onClick.AddListener (RestartGame);

		mainMenuButton.onClick.AddListener (GoToMainMenu);

		quitButton.onClick.AddListener (Application.Quit);

		nextButton.onClick.AddListener (NextLevel);

		if (GameState.GetCurrentState () != GameState.ResultType.WIN) {
			nextButton.enabled = false;
			nextButton.gameObject.SetActive (false);
		}

		UpdateText ();
	}

	private void UpdateText() {
		if (titleText != null) {
			if (GameState.GetCurrentState() == GameState.ResultType.GAME_OVER) {
				titleText.text = "GAME OVER";
			} else if (GameState.GetCurrentState() == GameState.ResultType.WIN) {
				titleText.text = "YOU WIN";
			}
		}
	}

	private void RestartGame(){
		PlayerState.Reset ();
		GameState.SetState (GameState.ResultType.INITIAL);
		SceneManager.LoadScene ("Gameplay-level-1", LoadSceneMode.Single);
	}

	private void GoToMainMenu(){
		PlayerState.Reset ();
		GameState.SetState (GameState.ResultType.INITIAL);
		SceneManager.LoadScene ("MainMenu", LoadSceneMode.Single);
	}

	private void NextLevel() {
		PlayerState.Reset ();
		GameState.SetState (GameState.ResultType.INITIAL);
		SceneManager.LoadScene("Gameplay-level-2");

	}
}
