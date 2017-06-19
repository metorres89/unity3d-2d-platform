using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandheldObjectController : MonoBehaviour {

	//this properties work checking if character is over a handheld object to use
	public Transform handheldCheck;
	public float handheldCheckRadius = 0.2f;
	public LayerMask handheldLayer;

	public GameObject handheldObject;
	public Transform handheldPosition;

	void Awake(){
		handheldObject = null;
	}

	void Start () {
		handheldCheck = gameObject.transform.Find ("GroundCheck").transform;
		handheldPosition = gameObject.transform.Find ("LazerOrigin").transform;
	}

	void FixedUpdate () {
		CheckIfIsOverHandheldObject ();
		//TODO - se debe agregar la lógica que permita arrojar el cuerpo tomado
		//TossObject ();
	}

	void Update(){
		if(handheldObject != null)
			handheldObject.transform.position = handheldPosition.position;
	}

	private void CheckIfIsOverHandheldObject() {
		if (handheldObject == null) {
			Collider2D[] colliders = Physics2D.OverlapCircleAll(handheldCheck.position, handheldCheckRadius, handheldLayer);
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i].gameObject != gameObject) {
					if(Input.GetAxis("Fire1") > 0){
						Debug.LogFormat ("Player wants to drag {0}", colliders [i].gameObject.tag);
						handheldObject = colliders [i].gameObject;
						return;
					}
				}
			}
		}
	}

	private void TossObject() {
		if (handheldObject != null && Input.GetAxis("Fire1") > 0) {
			handheldObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 20.0f);
		}
	}
}
