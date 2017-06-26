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
			Transform t = gameObject.transform.FindChild ("LifeText");
			if (t != null)
				lifeText = t.gameObject.GetComponent<Text> ();
		}

		if (scoreText == null) {
			Transform t = gameObject.transform.Find ("ScoreText");
			if (t != null)
				scoreText = t.gameObject.GetComponent<Text> ();
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
