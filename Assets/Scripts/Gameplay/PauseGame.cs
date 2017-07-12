using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {
	public GameObject pausePanel;
	public Button returnButton;
	public Button mainMenuButton;

	private bool isOnPause = false;
	private bool submitAxisInUse = false;

	void Start () {
		if(pausePanel == null)
		{
			pausePanel = GameObject.Find ("Canvas").transform.FindIgnoringActiveState ("PausePanel");
		}

		if (returnButton == null) {
			returnButton = pausePanel.transform.FindIgnoringActiveState ("ReturnButton").GetComponent<Button>();
		}

		if (mainMenuButton == null) {
			mainMenuButton = pausePanel.transform.FindIgnoringActiveState ("MainMenuButton").GetComponent<Button>();
		}

		if (returnButton != null)
			returnButton.onClick.AddListener (OnClickReturnButton);

		if (mainMenuButton != null)
			mainMenuButton.onClick.AddListener (GoToMainMenu);
	}
	
	void Update () {

		float submitAxis = Input.GetAxisRaw ("Submit");

		if(submitAxis > 0.0f && submitAxisInUse == false) {
			submitAxisInUse = true;
			TogglePause ();
		}

		if(submitAxis == 0.0f) {
			submitAxisInUse = false;
		}

		//Debug.LogFormat ("submitAxis: {0} - submitAxisInUse: {1} - isOnPause: {2}", submitAxis, submitAxisInUse, isOnPause);

	}

	private void OnClickReturnButton(){
		TogglePause ();
		submitAxisInUse = false;
	}

	private void TogglePause() {
		isOnPause = !isOnPause;
		pausePanel.SetActive (isOnPause);
		Time.timeScale = isOnPause ? 0.0f : 1.0f;
	}

	private void GoToMainMenu(){
		PlayerState.Reset ();
		GameState.Reset ();
		SceneManager.LoadScene ("MainMenu", LoadSceneMode.Single);
	}
}
