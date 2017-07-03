using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FXAudio
{
	private static AudioSource FxAudioSource;
	private static Dictionary<string, AudioClip> AudioClipDictionary;

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
				FxAudioSource.PlayOneShot (AudioClipDictionary [clipName], 0.5f);
			} else {
				Debug.LogWarning ("FXAudio - clipName doesn't exists in audioClipDictionay");
			}
		} else {
			Debug.LogWarning ("FXAudio - playClip - fxAudioSource is null");
		}
	}
}