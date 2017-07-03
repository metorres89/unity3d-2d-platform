using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	private Animator myAnimator;
	private Rigidbody2D myRigidBody;
	private float mySelfDestructDelay;
	private bool hasCollisioned;

	public float selfDestructDelay = 0.2f;
	public float xMaxLimit = 50.0f;
	public float xMinLimit = -50.0f;

	public void Awake() {
		mySelfDestructDelay = selfDestructDelay;
		hasCollisioned = false;
	}

	public void Start() {
		myAnimator = gameObject.GetComponent<Animator> ();
		myRigidBody = gameObject.GetComponent<Rigidbody2D> ();
	}

	public void Update() {

		if (gameObject.transform.position.x >= xMaxLimit || gameObject.transform.position.x <= xMinLimit) {
			DestroyImmediate (gameObject);
		} else {
			if (hasCollisioned) {
				mySelfDestructDelay -= Time.deltaTime;
				if (mySelfDestructDelay <= 0.0f) {
					DestroyImmediate (gameObject);
				}
			}
		}
	}

	public void OnCollisionEnter2D(Collision2D col) {

		myRigidBody.velocity = Vector2.zero;
		hasCollisioned = true;

		myAnimator.SetTrigger ("Impact");

		FXAudio.PlayClip ("Explosion", 0.5f);

		if (col.gameObject.tag == "Enemy") {
			ZombieController zc = col.gameObject.GetComponent<ZombieController> ();
			zc.ReceiveDamage (1.0f);
		}
	}
}
