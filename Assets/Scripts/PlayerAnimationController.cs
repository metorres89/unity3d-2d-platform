using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

	private Animator myAnimator;
	private SpriteRenderer mySpriteRenderer;
	private PlayerShoot myPlayerShoot;
	// Use this for initialization
	void Start () {
		myAnimator = GetComponent<Animator> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		myPlayerShoot = GetComponent<PlayerShoot> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		UpdateAnimationController ();
		Flip ();
	}

	void UpdateAnimationController()
	{
		myAnimator.SetFloat ("horizontalSpeed", Mathf.Abs(PlayerState.horizontalDirection));
		myAnimator.SetBool ("isOnGround", PlayerState.isOnGround);
		myAnimator.SetBool ("isShooting", PlayerState.isShooting);
		myAnimator.SetBool ("isDead", PlayerState.isDead);
	}

	void Flip(){
		
		if (PlayerState.horizontalDirection < 0) {
			mySpriteRenderer.flipX = true;
		} else if (PlayerState.horizontalDirection > 0) {
			mySpriteRenderer.flipX = false;
		}

		myPlayerShoot.FlipLazerOrigin (mySpriteRenderer.flipX);
	}
}
