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
	private bool onStun;
	private float myStunRecoveryTime;
	private float myGameOverDelay;
	private float myJumpDelay;
	private bool jumpAxisInUse;

	public float horizontalSpeed = 20.0f;
	public float jumpForce = 20.0f;
	public float jumpDelay = 1.0f;
	public float stunRecoveryTime = 0.5f;
	public float impactVerticalForce = 500.0f;
	public float smashEnemyHeadDamage = 1.0f;
	public float smashEnemyHeadBounceForce = 1000.0f;
	public float resistenceOnFalling = 50.0f;

	//this properties work checking if character is on ground
	public Transform groundCheck;
	public float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;
	public ParticleSystem hitParticleSystem;

	//this is a editable float at the inspector section, specify the remaining seconds after dead before the game over scene transition
	public float gameOverDelay = 2.0f;

	void Awake() {
		onStun = false;
		myStunRecoveryTime = stunRecoveryTime;
		myGameOverDelay = gameOverDelay;
		myJumpDelay = jumpDelay;
		jumpAxisInUse = false;
	}

	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		groundCheck = gameObject.transform.Find ("GroundCheck");
		myAnimator = GetComponent<Animator> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		myPlayerShoot = GetComponent<PlayerShoot> ();
		myPlayerGrabO = GetComponent<PlayerGrabObject> ();
	}

	void FixedUpdate () {
		if (PlayerState.HealthPoints > 0.0f) {
			CheckIfIsGrounded ();

			if (onStun) {
				myStunRecoveryTime -= Time.fixedTime;

				if (myStunRecoveryTime <= 0.0f) {
					onStun = false;
					myStunRecoveryTime = stunRecoveryTime;
				}
					
			}

		}
	}

	void Update () {
		if (PlayerState.HealthPoints > 0.0f) {
			CheckFlip ();
			HandleMove ();
		} else {
			myGameOverDelay -= Time.deltaTime;
			Debug.Log ("Player is dead, start countdown to ResultScene!!!");

			if (myGameOverDelay <= 0) {

				Debug.Log ("Player is dead, changing GameState");

				GameState.SetState(GameState.ResultType.GAME_OVER, "Result");
			}
		}

		UpdateAnimationController ();
	}

	void OnCollisionEnter2D(Collision2D col)
	{

		//Debug.LogFormat ("Colision against {0} relativeVelocity: {1}", col.gameObject.name, col.relativeVelocity);

		if (PlayerState.HealthPoints > 0.0f) {
			if (col.gameObject.tag == "Enemy" && col.contacts [0].normal.y > 0) {
				col.gameObject.GetComponent<ZombieController> ().ReceiveDamage (smashEnemyHeadDamage);
				myRigidbody.AddForce (Vector2.up * smashEnemyHeadBounceForce);
				myAnimator.SetTrigger ("triggerBounce");

				FXAudio.PlayClip ("PickupCoin");
			} else if(col.gameObject.tag == "ground" || col.gameObject.layer == groundLayer){
				if (Mathf.Abs (col.relativeVelocity.y) >= resistenceOnFalling) {
					ReceiveDamage (PlayerState.HealthPoints);
				}
			}
		}
	}

	private void HandleMove() {

		float jumpAxis;
		float horizontalAxis;

		horizontalAxis = Input.GetAxis ("Horizontal");
		jumpAxis = Input.GetAxis ("Jump");

		if (jumpAxisInUse)
			myJumpDelay -= Time.deltaTime;

		if (myJumpDelay <= 0.0f) {
			jumpAxisInUse = false;
			myJumpDelay = jumpDelay;
		}

		float velocityY = 0.0f;
		float velocityX = 0.0f;

		if (jumpAxis > 0 && PlayerState.IsOnGround && jumpAxisInUse == false) {
			jumpAxisInUse = true;
			velocityY = jumpForce;
			FXAudio.PlayClip ("Jump");
		} else {
			velocityY = myRigidbody.velocity.y;
		}

		if (horizontalAxis != 0.0f) {
			velocityX = horizontalAxis * horizontalSpeed;
		} else {
			velocityX = myRigidbody.velocity.x;
		}

		myRigidbody.velocity = new Vector2 (velocityX , velocityY);

		PlayerState.HorizontalDirection = horizontalAxis;
	}

	private void CheckIfIsGrounded() {
		bool foundGround = false;
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders [i].gameObject != gameObject) {
				foundGround = true;
			}
		}

		PlayerState.IsOnGround = foundGround;
	}

	private void UpdateAnimationController()
	{
		myAnimator.SetFloat ("horizontalSpeed", Mathf.Abs(PlayerState.HorizontalDirection));
		myAnimator.SetBool ("isOnGround", PlayerState.IsOnGround);
		myAnimator.SetBool ("isShooting", PlayerState.IsShooting);
		myAnimator.SetBool ("isDead", PlayerState.IsDead);
	}

	private void CheckFlip(){
		if (PlayerState.HorizontalDirection < 0 && mySpriteRenderer.flipX == false) {
			ExecutePlayerFlip (true);
		} else if (PlayerState.HorizontalDirection > 0 && mySpriteRenderer.flipX == true) {
			ExecutePlayerFlip (false);
		}
	}

	private void ExecutePlayerFlip(bool flipX){
		mySpriteRenderer.flipX = flipX;
		myPlayerShoot.Flip (mySpriteRenderer.flipX);
		myPlayerGrabO.Flip (mySpriteRenderer.flipX);
	}

	public void ReceiveDamage(float damage) {

		PlayHitParticleSystem ();

		if (!onStun) {
			PlayerState.HealthPoints -= damage;
			if (PlayerState.HealthPoints <= 0) {
				PlayerState.IsDead = true;
				FXAudio.PlayClip("Explosion");
			}
		}
	}

	public void ReceiveImpact(Vector2 force)
	{

		if (force.y == 0.0f)
			force.y = impactVerticalForce;
		
		onStun = true;
		myStunRecoveryTime = stunRecoveryTime;
		myRigidbody.AddForce (force);
		PlayHitParticleSystem ();
		FXAudio.PlayClip("Hit");
	}

	private void PlayHitParticleSystem() {
		if (!hitParticleSystem.isPlaying) {
			hitParticleSystem.Clear ();
			hitParticleSystem.Play ();
		}
	}
}
