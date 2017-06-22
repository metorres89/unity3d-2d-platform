﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	//movement character
	private Rigidbody2D myRigidbody;
	private Animator myAnimator;
	private SpriteRenderer mySpriteRenderer;
	private PlayerShoot myPlayerShoot;
	private PlayerGrabObject myPlayerGrabO;

	public float horizontalSpeed = 20.0f;
	public float jumpForce = 20.0f;

	//this properties work checking if character is on ground
	public Transform groundCheck;
	public float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;

	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		groundCheck = gameObject.transform.Find ("GroundCheck").transform;

		myAnimator = GetComponent<Animator> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		myPlayerShoot = GetComponent<PlayerShoot> ();
		myPlayerGrabO = GetComponent<PlayerGrabObject> ();
	}

	void FixedUpdate () {
		CheckIfIsGrounded ();
		HandleMove (Input.GetAxis ("Horizontal"), Input.GetAxis ("Jump"));
	}

	void Update () {
		UpdateAnimationController ();
		CheckFlip ();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy" && col.contacts[0].normal.y > 0) {

			col.gameObject.GetComponent<ZombieController> ().ReceiveDamage (1.0f);

		}
	}

	private void HandleMove(float horizontal, float jump) {
		float velocityOnYAxis = 0.0f;

		if (jump > 0 && PlayerState.isOnGround) {
			velocityOnYAxis = jumpForce;
		} else {
			velocityOnYAxis = myRigidbody.velocity.y;
		}

		myRigidbody.velocity = new Vector2 (horizontal * horizontalSpeed, velocityOnYAxis);

		PlayerState.horizontalDirection = horizontal;
	}

	private void CheckIfIsGrounded() {

		PlayerState.isOnGround = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				PlayerState.isOnGround = true;
		}

	}

	private void UpdateAnimationController()
	{
		myAnimator.SetFloat ("horizontalSpeed", Mathf.Abs(PlayerState.horizontalDirection));
		myAnimator.SetBool ("isOnGround", PlayerState.isOnGround);
		myAnimator.SetBool ("isShooting", PlayerState.isShooting);
		myAnimator.SetBool ("isDead", PlayerState.isDead);
	}

	private void CheckFlip(){
		if (PlayerState.horizontalDirection < 0 && mySpriteRenderer.flipX == false) {
			ExecutePlayerFlip (true);
		} else if (PlayerState.horizontalDirection > 0 && mySpriteRenderer.flipX == true) {
			ExecutePlayerFlip (false);
		}
	}

	private void ExecutePlayerFlip(bool flipX){
		mySpriteRenderer.flipX = flipX;
		myPlayerShoot.Flip (mySpriteRenderer.flipX);
		myPlayerGrabO.Flip (mySpriteRenderer.flipX);
	}
}