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
			GameObject go = GameObject.Find ("StartButton");
			if(go != null)
				startGameButton = go.GetComponent<Button> ();
		}	

		if (settingsButton == null) {
			GameObject go = GameObject.Find ("SettingsButton");
			if(go != null)
				settingsButton = go.GetComponent<Button> ();
		}

		startGameButton.onClick.AddListener (LoadGameplayScene);
		settingsButton.onClick.AddListener (ToggleSettingsPanel);
	}

	private void InitSettingsPanelAndButtons() {
		if (settingsPanel == null) {
			settingsPanel = gameObject.transform.FindIgnoringActiveState ("SettingsPanel");
		}

		if (fxVolumeSlider == null) {
			GameObject go = gameObject.transform.FindIgnoringActiveState ("FxVolumeSlider");
			if (go != null) {
				fxVolumeSlider = go.GetComponent<Slider> ();
			}
		}

		if (musicVolumeSlider == null) {
			GameObject go = gameObject.transform.FindIgnoringActiveState ("MusicVolumeSlider");
			if (go != null) {
				musicVolumeSlider = go.GetComponent<Slider> ();
			}
		}

		if (saveButton == null) {
			GameObject go = gameObject.transform.FindIgnoringActiveState ("SaveButton");
			if(go != null)
				saveButton = go.GetComponent<Button> ();
		}	

		if (discardButton == null) {
			GameObject go = gameObject.transform.FindIgnoringActiveState ("DiscardButton");
			if(go != null)
				discardButton = go.GetComponent<Button> ();
		}

		saveButton.onClick.AddListener (SaveSettings);
		discardButton.onClick.AddListener (LoadSettings);
		fxVolumeSlider.onValueChanged.AddListener (FxVolumeSliderValueChanged);
		musicVolumeSlider.onValueChanged.AddListener (MusicVolumeSliderValueChanged);
	}

	private void LoadGameplayScene() {
		SceneManager.LoadScene ("Gameplay", LoadSceneMode.Single);
	}

	private void ToggleSettingsPanel() {
		if (!settingsPanel.activeInHierarchy) {
			settingsPanel.SetActive (true);
			LoadSettings ();
		} else {
			settingsPanel.SetActive (false);
		}
	}

	private void SaveSettings() {
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
