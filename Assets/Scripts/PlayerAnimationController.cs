using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

	private Animator myAnimator;
	private SpriteRenderer mySpriteRenderer;
	private PlayerShoot myPlayerShoot;
	private HandheldObjectController myHandheldObjectController;
	// Use this for initialization
	void Start () {
		myAnimator = GetComponent<Animator> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		myPlayerShoot = GetComponent<PlayerShoot> ();

		myHandheldObjectController = GetComponent<HandheldObjectController> ();
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
		//flip sprite renderer
		if (PlayerState.horizontalDirection < 0) {
			mySpriteRenderer.flipX = true;
		} else if (PlayerState.horizontalDirection > 0) {
			mySpriteRenderer.flipX = false;
		}

		//flips
		myPlayerShoot.Flip (mySpriteRenderer.flipX);
		myHandheldObjectController.Flip (mySpriteRenderer.flipX);
	}
}
