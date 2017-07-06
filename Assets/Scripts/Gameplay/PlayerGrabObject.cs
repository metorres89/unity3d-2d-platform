using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabObject : MonoBehaviour {

	public Transform handheldCheck;
	public float handheldCheckRadius = 0.2f;
	public LayerMask handheldLayer;
	public GameObject handheldObject;
	public Transform handheldPosition;
	public float throwForce = 2000.0f;

	private float delayTakeObject = 0.5f;
	private SpriteRenderer hoSpriteRenderer;

	void Awake(){
		handheldObject = null;
		hoSpriteRenderer = null;
	}

	void Start () {
		handheldCheck = gameObject.transform.Find ("GroundCheck");
	}

	void FixedUpdate () {
		if (handheldObject == null) {
			CheckIfIsOverHandheldObject ();
		}
	}

	void Update() {
		if (handheldObject != null) {

			if (Input.GetAxis("Fire3") > 0.0f && delayTakeObject <= 0.0f) {
				
				ThrowObject ();

			} else {

				delayTakeObject -= Time.deltaTime;
			}
		}
	}

	private void CheckIfIsOverHandheldObject() {
		Collider2D[] colliders = Physics2D.OverlapCircleAll (handheldCheck.position, handheldCheckRadius, handheldLayer);

		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].gameObject != gameObject) {
				if(Input.GetAxis("Fire3") > 0.0f) {
					TakeObject (colliders [i].gameObject);
					return;
				}
			}
		}
	}

	private void TakeObject(GameObject target) {
		Debug.LogFormat ("Player wants to drag {0}", target.tag);

		handheldObject = target;
		handheldObject.transform.parent = handheldPosition;
		handheldObject.transform.localPosition = Vector2.zero;
		handheldObject.transform.position = handheldPosition.position;
		handheldObject.GetComponent<Rigidbody2D> ().simulated = false;

		hoSpriteRenderer = handheldObject.GetComponent<SpriteRenderer> ();
		SpriteRenderer playerSR = gameObject.GetComponent<SpriteRenderer> ();
		hoSpriteRenderer.flipX = playerSR.flipX;
	}

	private void ThrowObject() {
		if (handheldObject != null && Input.GetAxis("Fire3") > 0) {
			Rigidbody2D handheldRigidbody = handheldObject.GetComponent<Rigidbody2D> ();
			handheldObject.transform.parent = null;
			handheldObject.layer = LayerMask.NameToLayer("ThrownObject");
			handheldRigidbody.simulated = true;

			float hAxis = Input.GetAxis ("Horizontal");

			if (hAxis == 0.0f) {
				if (hoSpriteRenderer.flipX)
					hAxis = -1.0f;
				else
					hAxis = 1.0f;
			}

			handheldRigidbody.AddForce (new Vector2(hAxis, Input.GetAxis ("Vertical")) * throwForce);
			handheldObject = null;
			delayTakeObject = 0.5f;
		}
	}

	public void Flip(bool flipX) {
		if (handheldObject != null && hoSpriteRenderer != null) {
			if (flipX != hoSpriteRenderer.flipX)
				hoSpriteRenderer.flipX = flipX;
		}
	}
}
