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

	// Use this for initialization
	void Start () {
		MusicAudio.Init ();

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

		if (quitButton == null) {
			Transform t = gameObject.transform.Find ("QuitButton");
			if (t != null)
				quitButton = t.gameObject.GetComponent<Button> ();
		}


		if(restartButton != null)
			restartButton.onClick.AddListener (RestartGame);

		if(mainMenuButton != null)
			mainMenuButton.onClick.AddListener (GoToMainMenu);

		if (quitButton != null)
			quitButton.onClick.AddListener (Application.Quit);
		
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
		SceneManager.LoadScene ("Gameplay", LoadSceneMode.Single);
	}

	private void GoToMainMenu(){
		PlayerState.Reset ();
		GameState.SetState (GameState.ResultType.INITIAL);
		SceneManager.LoadScene ("MainMenu", LoadSceneMode.Single);
	}
}
