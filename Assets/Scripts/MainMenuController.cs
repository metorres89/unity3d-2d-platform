using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public Button startGameButton;
	public Button settingsButton;

	void Start () {
		initMainMenuButtons ();
	}

	private void initMainMenuButtons() {
		if (startGameButton == null) {
			Transform t = gameObject.transform.Find ("StartButton");
			if(t != null)
				startGameButton = t.gameObject.GetComponent<Button> ();
		}	

		if (settingsButton == null) {
			Transform t = gameObject.transform.Find ("SettingsButton");
			if(t != null)
				settingsButton = t.gameObject.GetComponent<Button> ();
		}

		if (startGameButton != null) {
			startGameButton.onClick.AddListener (executeTransitionToGameplayScene);
		}

		if (settingsButton != null) {
			settingsButton.onClick.AddListener (showSettings);
		}
	}

	private void executeTransitionToGameplayScene() {

		Debug.Log ("execute transition to gameplay scene!");

		SceneManager.LoadScene ("Gameplay", LoadSceneMode.Single);

	}

	private void showSettings() {
		Debug.Log ("Show settings panel!");
	}
}
