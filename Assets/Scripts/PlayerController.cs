using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	//movement character
	private Rigidbody2D myRigidbody;
	private Animator myAnimator;
	private SpriteRenderer mySpriteRenderer;
	private PlayerShoot myPlayerShoot;
	private PlayerGrabObject myPlayerGrabO;
	private FXAudioSourceController myFXAudioSourceController;

	private bool onStun = false;
	private float myStunRecoveryTime = 0.0f;

	public float horizontalSpeed = 20.0f;
	public float jumpForce = 20.0f;
	public float stunRecoveryTime = 0.5f;
	public float impactVerticalForce = 500.0f;
	public float smashEnemyHeadDamage = 1.0f;
	public float smashEnemyHeadBounceForce = 1000.0f;

	//this properties work checking if character is on ground
	public Transform groundCheck;
	public float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;

	public AudioClip jumpClip;
	public AudioClip receiveImpactClip;
	public AudioClip powerUpClip;
	public AudioClip scoreClip;
	//public AudioClip shootClip;
	public AudioClip deadClip;

	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		groundCheck = gameObject.transform.Find ("GroundCheck");

		myAnimator = GetComponent<Animator> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		myPlayerShoot = GetComponent<PlayerShoot> ();
		myPlayerGrabO = GetComponent<PlayerGrabObject> ();

		if (myFXAudioSourceController == null) {
			Transform t = Camera.main.transform.Find ("FXAudioSource");
			if (t != null)
				myFXAudioSourceController = t.gameObject.GetComponent<FXAudioSourceController> ();
		}

		myStunRecoveryTime = stunRecoveryTime;
	}

	void FixedUpdate () {
		if (PlayerState.HP > 0.0f) {
			CheckIfIsGrounded ();

			if (onStun) {
				myStunRecoveryTime -= Time.fixedTime;

				if (myStunRecoveryTime <= 0.0f) {
					onStun = false;
					myStunRecoveryTime = stunRecoveryTime;
				}
					
			} else {
				HandleMove (Input.GetAxis ("Horizontal"), Input.GetAxis ("Jump"));
			}
		}
	}

	void Update () {
		if (PlayerState.HP > 0.0f) {
			CheckFlip ();
		}

		UpdateAnimationController ();

		//DEBUG
		if (Input.GetKeyDown (KeyCode.T)) {
			Debug.Log ("add force to rigidb");
			myRigidbody.AddForce (Vector2.right * 2000.0f, ForceMode2D.Force);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (PlayerState.HP > 0.0f) {
			if (col.gameObject.tag == "Enemy" && col.contacts [0].normal.y > 0) {
				col.gameObject.GetComponent<ZombieController> ().ReceiveDamage (smashEnemyHeadDamage);
				myRigidbody.AddForce (Vector2.up * smashEnemyHeadBounceForce);
				myAnimator.SetTrigger ("triggerBounce");

				myFXAudioSourceController.playClip (scoreClip, 0.5f);
			}
		}
	}

	private void HandleMove(float horizontal, float jump) {
		float velocityY = 0.0f;
		float velocityX = 0.0f;

		if (jump > 0 && PlayerState.isOnGround) {
			velocityY = jumpForce;

			myFXAudioSourceController.playClip (jumpClip, 0.5f);

		} else {
			velocityY = myRigidbody.velocity.y;
		}

		if (horizontal != 0.0f) {
			velocityX = horizontal * horizontalSpeed;
		} else {
			velocityX = myRigidbody.velocity.x;
		}

		myRigidbody.velocity = new Vector2 (velocityX , velocityY);

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

	public void ReceiveDamage(float damage) {

		PlayerState.HP -= damage;

		if (PlayerState.HP <= 0) {
			PlayerState.isDead = true;

			myFXAudioSourceController.playClip (deadClip, 0.5f);
		}

	}

	public void ReceiveImpact(Vector2 point, float force)
	{
		Debug.LogFormat ("PlayerController.ReceiveImpact - player is receiving an impact - force to apply:{0}", force);

		Vector2 resultForce = new Vector2 (point.x * -1 * force, impactVerticalForce);

		Debug.LogFormat ("PlayerController.ReceiveImpact - player will be hit by a force vector: {0}", resultForce);

		onStun = true;
		myStunRecoveryTime = stunRecoveryTime;

		myRigidbody.AddForce (resultForce);

		myFXAudioSourceController.playClip (receiveImpactClip, 0.5f);
	}
}
