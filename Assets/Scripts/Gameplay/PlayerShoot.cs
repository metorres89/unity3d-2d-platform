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
		Debug.Log ("holaaa");
		if(PlayerState.HealthPoints > 0.0f)
		{
			if (hasShoot) {
				myShootDelay -= Time.deltaTime;
			}

			if (myShootDelay <= 0) {
				hasShoot = false;
				myShootDelay = shootDelay;
			}

			if (Input.GetAxis("Fire1") > 0.0f && hasShoot == false) {
				hasShoot = true;
				FXAudio.PlayClip ("Shoot");
				CreateNewBullet();
				PlayerState.IsShooting = true;
			}

			if (Input.GetAxis("Fire1") == 0.0f) {
				PlayerState.IsShooting = false;
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
