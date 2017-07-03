using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FXAudio
{
	private static AudioSource FxAudioSource;
	private static Dictionary<string, AudioClip> AudioClipDictionary;
	private static float FxVolume;

	public static void Init() {
		if (FxAudioSource == null) {
			GameObject go = GameObject.Find("FXAudioSource");
			if (go != null)
				FxAudioSource = go.GetComponent<AudioSource> ();
			else
				Debug.LogWarningFormat ("FXAudio - Init - There is no FXAudioSource GameObject with AudioSource component attached.");
		}

		if (AudioClipDictionary == null) {
			AudioClipDictionary = new Dictionary<string, AudioClip> ();
			LoadClipsFromResources ("Sounds");
		}

		ReloadSettingsFromPlayerPrefs ();
	}

	public static void LoadClipsFromResources(string folderName){

		AudioClip[] audioClipArray = Resources.LoadAll<AudioClip> (folderName);

		foreach (AudioClip clip in audioClipArray) {
			if (!AudioClipDictionary.ContainsKey (clip.name)) {
				AudioClipDictionary.Add (clip.name, clip);
			} else {
				Debug.LogWarningFormat ("FXAudio - LoadClipsFromResources - Duplicated audioclips on folder: '{0}'", folderName);
			}
		}
	}

	public static void PlayClip(string clipName) {
		if (FxAudioSource != null) {
			if (AudioClipDictionary.ContainsKey (clipName)) {
				FxAudioSource.PlayOneShot (AudioClipDictionary [clipName], FxVolume);
			} else {
				Debug.LogWarning ("FXAudio - clipName doesn't exists in audioClipDictionay");
			}
		} else {
			Debug.LogWarning ("FXAudio - playClip - fxAudioSource is null");
		}
	}

	public static void ReloadSettingsFromPlayerPrefs() {
		if (PlayerPrefs.HasKey ("Audio.Fx.Volume")) {
			FxVolume = PlayerPrefs.GetFloat ("Audio.Fx.Volume");
		} else {
			FxVolume = 0.5f;
		}
	}

	public static void SetVolume(float volume) {
		FxVolume = Mathf.Clamp(volume, 0.0f, 1.0f);
	}
}