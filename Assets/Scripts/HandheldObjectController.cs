﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandheldObjectController : MonoBehaviour {

	//this properties work checking if character is over a handheld object to use
	public Transform handheldCheck;
	public float handheldCheckRadius = 0.2f;
	public LayerMask handheldLayer;

	public GameObject handheldObject;
	public Transform handheldPosition;

	private float delayTakeObject = 0.5f;

	void Awake(){
		handheldObject = null;
	}

	void Start () {
		handheldCheck = gameObject.transform.Find ("GroundCheck").transform;
	}

	void FixedUpdate () {
		if (handheldObject == null) {
			CheckIfIsOverHandheldObject ();
		}
	}

	void Update() {
		if (handheldObject != null) {

			if (Input.GetKeyDown(KeyCode.LeftControl) && delayTakeObject <= 0.0f) {
				Debug.Log ("player wants to toss de object!");

				TossObject ();

			} else {

				delayTakeObject -= Time.deltaTime;
			}
		}
	}

	private void CheckIfIsOverHandheldObject() {
			Collider2D[] colliders = Physics2D.OverlapCircleAll(handheldCheck.position, handheldCheckRadius, handheldLayer);
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i].gameObject != gameObject) {
					if(Input.GetKeyDown(KeyCode.LeftControl)) {
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
	}

	private void TossObject() {
		if (handheldObject != null && Input.GetAxis("Fire1") > 0) {

			Rigidbody2D handheldRigidbody = handheldObject.GetComponent<Rigidbody2D> ();
			handheldObject.transform.parent = null;
			handheldRigidbody.simulated = true;
			handheldRigidbody.AddForce (Vector2.up * 100.0f);

			handheldObject = null;
			delayTakeObject = 0.5f;
		}
	}
}
