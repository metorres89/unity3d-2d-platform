using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FXAudio
{
	private static AudioSource fxAudioSource;
	private static Dictionary<string, AudioClip> audioClipDictionary;

	public static void Init() {
		if (fxAudioSource == null) {
			GameObject go = GameObject.Find("FXAudioSource");
			if (go != null)
				fxAudioSource = go.GetComponent<AudioSource> ();
			else
				Debug.LogWarningFormat ("FXAudio - Init - There is no FXAudioSource GameObject with AudioSource component attached.");
		}

		if (audioClipDictionary == null) {
			audioClipDictionary = new Dictionary<string, AudioClip> ();
			LoadClipsFromResources ("Sounds");
		}

	}

	public static void LoadClipsFromResources(string folderName){

		AudioClip[] audioClipArray = Resources.LoadAll<AudioClip> (folderName);

		foreach (AudioClip clip in audioClipArray) {
			if (!audioClipDictionary.ContainsKey (clip.name)) {
				audioClipDictionary.Add (clip.name, clip);
			} else {
				Debug.LogWarningFormat ("FXAudio - LoadClipsFromResources - Duplicated audioclips on folder: '{0}'", folderName);
			}
		}
	}

	public static void playClip(string clipName, float volume) {
		if (fxAudioSource != null) {
			if (audioClipDictionary.ContainsKey (clipName)) {
				fxAudioSource.PlayOneShot (audioClipDictionary [clipName], volume);
			} else {
				Debug.LogWarning ("FXAudio - clipName doesn't exists in audioClipDictionay");
			}
		} else {
			Debug.LogWarning ("FXAudio - playClip - fxAudioSource is null");
		}
	}
}