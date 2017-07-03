using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MusicAudio
{
	private static AudioSource MyAudioSource;
	private static float MyVolume;

	public static void Init() {
		if (MyAudioSource == null) {
			GameObject go = GameObject.Find("MusicAudioSource");
			if (go != null)
				MyAudioSource = go.GetComponent<AudioSource> ();
			else
				Debug.LogWarningFormat ("MusicAudio - Init - There is no MusicAudioSource GameObject with AudioSource component attached.");
		}

		ReloadSettingsFromPlayerPrefs ();
	}

	public static void ReloadSettingsFromPlayerPrefs() {
		if (PlayerPrefs.HasKey ("Audio.Music.Volume")) {
			MyVolume = PlayerPrefs.GetFloat ("Audio.Music.Volume");
		} else {
			MyVolume = 0.5f;
		}

		MyAudioSource.volume = MyVolume;
	}

	public static void SetVolume(float volume) {
		MyVolume = Mathf.Clamp(volume, 0.0f, 1.0f);
		MyAudioSource.volume = MyVolume;
	}
}