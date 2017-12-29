using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Prototype : MonoBehaviour {

	public int playerSpeed = 10;
	public int playerJumpPower = 1250;
	private float moveX;

	// Update is called once per frame
	void Update () {
		PlayerMove();
	}

	void PlayerMove() {
		// Controls
		moveX = Input.GetAxis("Horizontal");


		// Jumping
		if (Input.GetButtonDown ("Jump")) {
			Jump();
			GetComponent<Animator> ().SetBool ("isJumping", true);
		} 

		if (gameObject.GetComponent<Rigidbody2D> ().velocity.y < 0) {
			// Player is falling
			GetComponent<Animator> ().SetBool ("isJumping", false);
			GetComponent<Animator> ().SetBool ("isFalling", true);
		} else {
			GetComponent<Animator> ().SetBool ("isFalling", false);
		}

		// Punching
		if (Input.GetButtonDown ("Fire1")) {
			GetComponent<Animator> ().SetBool ("isPunching", true);
			moveX = (float) (0.5*moveX);	// Slow down while punching
		} else {
			GetComponent<Animator> ().SetBool ("isPunching", false);
		}

	
		// Walking/Idle Animations
		if (moveX != 0) {
			GetComponent<Animator> ().SetBool ("isWalking", true);
		} else {
			GetComponent<Animator> ().SetBool ("isWalking", false);
		}



		// Player Direction
		if (moveX < 0.0f) {
			// Flip player
			GetComponent<SpriteRenderer> ().flipX = true;
		} else if (moveX > 0.0f) {
			GetComponent<SpriteRenderer> ().flipX = false;
		}

		// Physics
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
	}

	void Jump(){
		// Jumping Code
		GetComponent<Rigidbody2D>().AddForce (Vector2.up * playerJumpPower);
	}


}

