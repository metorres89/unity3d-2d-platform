using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	public float lazerLength = 10.0f;
	public Transform lazerOrigin;
	public GameObject player;
	public float lazerDelay = 0.5f;

	private Vector3 offset;
	private bool myFlipX = false;
	private float lazerTimer;

	void Start(){

		if (lazerOrigin == null) {
			lazerOrigin = gameObject.transform.Find ("LazerOrigin");
		}

		if (lazerOrigin != null && player != null) {
			offset = lazerOrigin.position - player.transform.position;
		}

		lazerTimer = lazerDelay;
	}

	void Update () {

		if (Input.GetMouseButton (0)) {
			PlayerState.isShooting = true;

			if (lazerTimer <= 0) {
				ComputeShoot ();
				lazerTimer = lazerDelay;
			}

			lazerTimer -= Time.deltaTime;
		}

		if (Input.GetMouseButtonUp (0)) {
			PlayerState.isShooting = false;
		}

	}

	void ComputeShoot(){

		Vector3 screenPos = Input.mousePosition;

		screenPos.z = 20.0f;

		Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

		Vector2 rayOrigin = new Vector2 (lazerOrigin.position.x, lazerOrigin.position.y);
		Vector2 rayDestiny = new Vector2 (worldPos.x, worldPos.y);

		if(( !myFlipX && rayDestiny.x > rayOrigin.x) || (myFlipX && rayDestiny.x < rayOrigin.x) ){

			Vector2 rayDirection = rayDestiny - rayOrigin;
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, rayDirection);

			//Debug.DrawLine (lazerOrigin.position, worldPos, Color.red, 0.5f);
			Debug.DrawRay (rayOrigin, rayDirection, Color.blue, 0.5f);

			if (hit) {
				Debug.LogFormat ("We hit something!!! {0} {1}", hit.collider.gameObject.name, hit.collider.gameObject.tag);
			}
		}
	}

	public void Flip(bool flip)
	{
		if (flip != myFlipX) {
			myFlipX = flip;

			Vector3 newPos;

			if (myFlipX)
				newPos = player.transform.position - offset;
			else
				newPos = player.transform.position + offset;
			
			lazerOrigin.position = newPos;
		}
	}
}
