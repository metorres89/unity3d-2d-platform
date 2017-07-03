using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	//public float lazerLength = 10.0f;
	public Transform bulletOrigin;
	public GameObject bulletPrototype;
	public float bulletSpeed = 50.0f;
	public GameObject player;
	public float shootDelay = 0.5f;

	private Vector3 myOffset;
	private bool myFlipX = false;
	private float myShootDelay;
	private bool hasShoot;

	void Start(){

		if (bulletOrigin == null) {
			bulletOrigin = gameObject.transform.Find ("BulletOrigin");
		}

		if (bulletOrigin != null && player != null) {
			myOffset = bulletOrigin.position - player.transform.position;
		}

		myShootDelay = shootDelay;
		hasShoot = false;
	}

	void Update () {

		if (hasShoot) {
			myShootDelay -= Time.deltaTime;
		}

		if (myShootDelay <= 0) {
			hasShoot = false;
			myShootDelay = shootDelay;
		}

		if (Input.GetMouseButton (0) && hasShoot == false) {
			hasShoot = true;
			FXAudio.PlayClip ("Shoot");
			CreateNewBullet();
			PlayerState.IsShooting = true;
		}

		if (Input.GetMouseButtonUp (0)) {
			PlayerState.IsShooting = false;
		}
	}

	void ComputeShoot(){

		Vector3 screenPos = Input.mousePosition;

		screenPos.z = 20.0f;

		Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

		Vector2 rayOrigin = new Vector2 (bulletOrigin.position.x, bulletOrigin.position.y);
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

	public void CreateNewBullet() {
		GameObject newBullet = Instantiate (bulletPrototype, bulletOrigin.position, Quaternion.identity);

		Vector2 bulletDirection = Vector2.right;

		if (myFlipX) {
			newBullet.GetComponent<SpriteRenderer> ().flipX = myFlipX;
			bulletDirection = Vector2.left;
		}

		newBullet.GetComponent<Rigidbody2D> ().velocity = bulletDirection * bulletSpeed;
	}

	public void Flip(bool flip)
	{
		if (flip != myFlipX) {
			myFlipX = flip;

			Vector3 newPos;

			if (myFlipX)
				newPos = player.transform.position - myOffset;
			else
				newPos = player.transform.position + myOffset;
			
			bulletOrigin.position = newPos;
		}
	}
}
