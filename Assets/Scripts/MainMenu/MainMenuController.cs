using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public Button startGameButton;
	public Button settingsButton;
	public Button saveButton;
	public Button discardButton;

	public GameObject settingsPanel;

	public Slider fxVolumeSlider;
	public Slider musicVolumeSlider;

	void Start () {

		FXAudio.Init ();
		MusicAudio.Init ();

		InitMainMenuButtons ();
		InitSettingsPanelAndButtons ();
		LoadSettings ();
	}

	private void InitMainMenuButtons() {
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

		if (fxVolumeSlider == null) {
			GameObject go = gameObject.transform.FindGameObject ("FxVolumeSlider");
			if (go != null) {
				fxVolumeSlider = go.GetComponent<Slider> ();
			}
		}

		if (musicVolumeSlider == null) {
			GameObject go = gameObject.transform.FindGameObject ("MusicVolumeSlider");
			if (go != null) {
				musicVolumeSlider = go.GetComponent<Slider> ();
			}
		}

		if (startGameButton != null) {
			startGameButton.onClick.AddListener (LoadGameplayScene);
		}

		if (settingsButton != null) {
			settingsButton.onClick.AddListener (ToggleSettingsPanel);
		}

		if (fxVolumeSlider != null) {
			fxVolumeSlider.onValueChanged.AddListener (FxVolumeSliderValueChanged);
		}

		if (musicVolumeSlider != null) {
			musicVolumeSlider.onValueChanged.AddListener (MusicVolumeSliderValueChanged);
		}

	}

	private void InitSettingsPanelAndButtons() {
		if (settingsPanel == null) {
			settingsPanel = gameObject.transform.FindGameObject ("SettingsPanel");
		}
			
		if (saveButton == null) {
			GameObject go = GameObject.Find ("SaveButton");
			if(go != null)
				saveButton = go.GetComponent<Button> ();
		}	

		if (discardButton == null) {
			GameObject go = GameObject.Find ("DiscardButton");
			if(go != null)
				discardButton = go.GetComponent<Button> ();
		}

		if (saveButton != null) {
			saveButton.onClick.AddListener (SaveSettings);
		}

		if (discardButton != null) {
			discardButton.onClick.AddListener (LoadSettings);
		}

	}

	private void LoadGameplayScene() {
		SceneManager.LoadScene ("Gameplay", LoadSceneMode.Single);
	}

	private void ToggleSettingsPanel() {
		Debug.Log ("Toggle settings panel!");

		if (!settingsPanel.activeInHierarchy) {
			settingsPanel.SetActive (true);
			LoadSettings ();
		} else {
			settingsPanel.SetActive (false);
		}
	}

	private void SaveSettings() {
		Debug.Log ("Save Settings!");

		if (fxVolumeSlider != null) {
			PlayerPrefs.SetFloat ("Audio.Fx.Volume", fxVolumeSlider.value / fxVolumeSlider.maxValue);
		}

		if (musicVolumeSlider != null) {
			PlayerPrefs.SetFloat ("Audio.Music.Volume", musicVolumeSlider.value / musicVolumeSlider.maxValue);
		}
	}

	private void LoadSettings() {
		if (PlayerPrefs.HasKey ("Audio.Fx.Volume")) {
			if (fxVolumeSlider != null) {
				fxVolumeSlider.value = PlayerPrefs.GetFloat ("Audio.Fx.Volume") * fxVolumeSlider.maxValue;
			}
		}

		if (PlayerPrefs.HasKey ("Audio.Music.Volume")) {
			if (musicVolumeSlider != null) {
				musicVolumeSlider.value = PlayerPrefs.GetFloat ("Audio.Music.Volume") * musicVolumeSlider.maxValue;
			}
		}
	}

	private void FxVolumeSliderValueChanged(float value) {
		if (fxVolumeSlider != null) {
			FXAudio.SetVolume (value / fxVolumeSlider.maxValue);
			FXAudio.PlayClip ("Hit");
		}
	}

	private void MusicVolumeSliderValueChanged(float value) {
		if (musicVolumeSlider != null) {
			MusicAudio.SetVolume (value / musicVolumeSlider.maxValue);
		}
	}
}
