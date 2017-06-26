using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {
	
	public Text lifeText;
	public Text scoreText;

	// Use this for initialization
	void Start () {

		if (lifeText == null) {
			GameObject go = gameObject.transform.FindChild ("LifeText").gameObject;
			if (go != null)
				lifeText = go.GetComponent<Text> ();
		}

		if (scoreText == null) {
			GameObject go = gameObject.transform.Find ("ScoreText").gameObject;
			if (go != null)
				scoreText = go.GetComponent<Text> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (lifeText != null)
			lifeText.text = string.Format ("Lifes: {0}", PlayerState.HP);

		if (scoreText != null)
			scoreText.text = string.Format ("Score: {0}", PlayerState.killedEnemies);
	}
}
