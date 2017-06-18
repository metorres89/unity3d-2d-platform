using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombieMovement : MonoBehaviour {

	private Vector2 leftLimit;
	private Vector2 rightLimit;
	private Vector2 movementDirection;
	private Rigidbody2D myRigidbody;
	private SpriteRenderer mySpriteRenderer;
	private Animator myAnimator;

	public float movementSpeed = 2.0f;

	void Awake()
	{
		leftLimit = Vector2.zero;
		rightLimit = Vector2.zero;
		movementDirection = Vector2.right;
		myRigidbody = gameObject.GetComponent<Rigidbody2D> ();
		mySpriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		myAnimator = gameObject.GetComponent<Animator> ();

	}
	
	void FixedUpdate () {
		MoveEnemyToDirection ();
	}

	void Update() {
		UpdateAnimator ();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Debug.LogFormat ("OnCollisionEnter2D - gameobject name {0} tag {1}", col.gameObject.name, col.gameObject.tag);

		if (col.gameObject.tag == "ground" && col.contacts [0].collider.sharedMaterial == null) {

			SetMovementLimits (col.contacts [0]);

		} else {

			if (col.gameObject.tag == "Player") {
				
				//Check collision conditions to perform attack animation

				//Attack();
			}else{
				Flip ();
			}
		}
	}

	void OnCollisionExit2D(Collision2D col){
		Debug.LogFormat ("OnCollisionEnter2D - gameobject name {0} tag {1}", col.gameObject.name, col.gameObject.tag);

		if (col.gameObject.tag == "ground" && col.contacts [0].collider.sharedMaterial == null) {
			ResetMovementLimits();
		}
	}

	private void ResetMovementLimits(){
		leftLimit = Vector2.zero;
		rightLimit = Vector2.zero;
	}

	private void UpdateAnimator() {
		myAnimator.SetFloat ("hSpeed", Mathf.Abs(myRigidbody.velocity.x));
		myAnimator.SetBool ("isDead", false);
		myAnimator.SetBool ("isAttacking", false);
	}
		
	private void MoveEnemyToDirection() {
		if (leftLimit != Vector2.zero && rightLimit != Vector2.zero && movementDirection != Vector2.zero) {

			if ((movementDirection == Vector2.right && transform.position.x < rightLimit.x) || (movementDirection == Vector2.left && transform.position.x > leftLimit.x)) {
				myRigidbody.velocity = movementDirection * movementSpeed;
			} else {
				Flip ();
			}

		}
	}

	private void Flip()
	{
		if (movementDirection == Vector2.right) {
			movementDirection = Vector2.left;
			mySpriteRenderer.flipX = true;
		} else {
			movementDirection = Vector2.right;
			mySpriteRenderer.flipX = false;
		}
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
}
