using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXAudioSourceController : MonoBehaviour {

	public AudioSource fxAudioSource;

	// Use this for initialization
	void Start () {
		if (fxAudioSource == null) {
			fxAudioSource = gameObject.GetComponent<AudioSource> ();
		}
	}

	public void playClip(AudioClip clip, float volume){
		if (fxAudioSource != null && clip != null && volume > 0.0f) {
			fxAudioSource.PlayOneShot (clip, volume);
		}
	}
}
