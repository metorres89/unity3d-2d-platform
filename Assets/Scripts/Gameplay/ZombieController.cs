﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

	private Vector2 leftLimit;
	private Vector2 rightLimit;
	private Vector2 movementDirection;
	private Rigidbody2D myRigidbody;
	private SpriteRenderer mySpriteRenderer;
	private Animator myAnimator;
	private CapsuleCollider2D myOnLiveCollider;
	private BoxCollider2D myOnDeadCollider;
	private float myWalkingSpeed;
	private float myFollowingPlayerDelay;

	public float healthPoints = 1.0f;
	public float walkingSpeed = 2.0f;
	public float attackForce = 2000.0f;
	public float attackDamage = 1.0f;
	public bool isFollowingPlayer = false;
	public float followingPlayerDelay = 1.0f;
	public ParticleSystem bloodParticleSystem;

	void Awake()
	{
		leftLimit = Vector2.zero;
		rightLimit = Vector2.zero;
		movementDirection = Vector2.right;
		myRigidbody = gameObject.GetComponent<Rigidbody2D> ();
		mySpriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		myAnimator = gameObject.GetComponent<Animator> ();
		myOnLiveCollider = gameObject.GetComponent<CapsuleCollider2D> ();
		myOnDeadCollider = gameObject.GetComponent<BoxCollider2D> ();
		myWalkingSpeed = walkingSpeed;
		myFollowingPlayerDelay = followingPlayerDelay;
	}

	void Start() {
		if (bloodParticleSystem == null) {
			Transform t = gameObject.transform.Find ("BloodParticleSystem");

			if (t != null) {
				bloodParticleSystem = t.gameObject.GetComponent<ParticleSystem> ();
			}
		}
	}
	
	void FixedUpdate () {
		if (healthPoints > 0) {
			MoveEnemyToDirection ();
		}
	}

	void Update() {
		if (healthPoints > 0) {
			myAnimator.SetFloat ("hSpeed", Mathf.Abs (myRigidbody.velocity.x));
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (healthPoints > 0) {

			if (col.gameObject.tag == "ground" && col.contacts [0].collider.sharedMaterial == null) {

				SetMovementLimits (col.contacts [0]);

			} else if (col.gameObject.tag == "Player") {
				
				if (Mathf.Approximately(col.contacts [0].normal.x, 1.0f) || Mathf.Approximately(col.contacts [0].normal.x, -1.0f) )  {
					Attack (col.gameObject, col.contacts [0].normal);
				}

			}else if(col.gameObject.layer == LayerMask.NameToLayer("ThrownObject")) {

				if (Mathf.Abs (col.rigidbody.velocity.x) > 10.0f) {
					ReceiveDamage (1.0f);
				}

			} else {
				Flip ();
			}
		}

		FXAudio.PlayClip ("Hit");
	}

	void OnCollisionStay2D(Collision2D col) {
		if (healthPoints <= 0) {
			if (gameObject.layer == LayerMask.NameToLayer ("ThrownObject") && myRigidbody.velocity == Vector2.zero) {
				gameObject.layer = LayerMask.NameToLayer ("Handheld");
			}
		}
	}

	void OnCollisionExit2D(Collision2D col){
		if (healthPoints > 0) {
			if (col.gameObject.tag == "ground" && col.contacts [0].collider.sharedMaterial == null) {
				ResetMovementLimits ();
			} else if (col.gameObject.tag == "Player") {

				myAnimator.SetBool ("isAttacking", false);
			}
		}
	}

	private void Attack(GameObject target, Vector2 attackPoint){
		if ((movementDirection == Vector2.right && Mathf.Approximately(attackPoint.x, 1.0f)) || (movementDirection == Vector2.left && Mathf.Approximately(attackPoint.x, -1.0f))) {
			Flip ();
		}

		myRigidbody.velocity = Vector2.zero;
		myAnimator.SetBool ("isAttacking", true);

		PlayerController pc = target.GetComponent<PlayerController> ();
		pc.ReceiveDamage (attackDamage);

		Vector2 impactForce = new Vector2 (attackPoint.x * -1 * attackForce, 0.0f);
		pc.ReceiveImpact (impactForce);
	}

	private void ResetMovementLimits(){
		leftLimit = Vector2.zero;
		rightLimit = Vector2.zero;
	}
		
	private void MoveEnemyToDirection() {
		if (leftLimit != Vector2.zero && rightLimit != Vector2.zero && movementDirection != Vector2.zero) {
			if ( ((movementDirection == Vector2.right && transform.position.x < rightLimit.x) || (movementDirection == Vector2.left && transform.position.x > leftLimit.x)) || isFollowingPlayer ) {
				myRigidbody.velocity = movementDirection * myWalkingSpeed;
			} else {
				Flip ();
			}
		}

		if (isFollowingPlayer) {
			myFollowingPlayerDelay -= Time.fixedDeltaTime;

			if (myFollowingPlayerDelay <= 0.0f) {
				isFollowingPlayer = false;
				myFollowingPlayerDelay = followingPlayerDelay;
			}
		}
	}

	public void Flip()
	{
		if (movementDirection == Vector2.right) {
			movementDirection = Vector2.left;
			mySpriteRenderer.flipX = true;
		} else {
			movementDirection = Vector2.right;
			mySpriteRenderer.flipX = false;
		}
	}

	public bool IsFlipOnX() {
		return mySpriteRenderer.flipX;
	}

	public Vector2 GetMovementDirection(){
		return movementDirection;
	}

	private void SetMovementLimits(ContactPoint2D contactPoint)
	{
		if (leftLimit == Vector2.zero && rightLimit == Vector2.zero) {

			leftLimit = new Vector2 (contactPoint.collider.bounds.min.x, gameObject.transform.position.y);
			rightLimit = new Vector2 (contactPoint.collider.bounds.max.x, gameObject.transform.position.y);

			if (Vector2.Distance (contactPoint.point, leftLimit) > Vector2.Distance (contactPoint.point, rightLimit)) {
				Flip ();
			}

		}
	}

	public void ReceiveDamage(float damage){
		healthPoints -= damage;

		if (!bloodParticleSystem.isPlaying) {
			bloodParticleSystem.Clear ();
			bloodParticleSystem.Play ();
		}

		if (healthPoints <= 0) {
			SetToDeadState ();
		}
	}

	private void SetToDeadState() {
		movementDirection = Vector2.zero;
		myRigidbody.velocity = Vector2.zero;
		myAnimator.SetBool ("isAttacking", false);
		myAnimator.SetBool ("isDead", true);
		myOnLiveCollider.enabled = false;
		myOnDeadCollider.enabled = true;
		gameObject.layer = LayerMask.NameToLayer("Handheld");

		PlayerState.KilledEnemies++;
	}

	public void SetWalkingSpeed(float newSpeed){
		myWalkingSpeed = newSpeed;
	}

	public float GetWalkingSpeed(){
		return myWalkingSpeed;
	}

	public void ResetWalkingSpeed(){
		myWalkingSpeed = walkingSpeed;
	}
}
