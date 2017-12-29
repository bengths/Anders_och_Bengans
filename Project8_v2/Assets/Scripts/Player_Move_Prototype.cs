using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Prototype : MonoBehaviour {

	public int playerSpeed = 10;
	public int playerJumpPower = 1250;
	private float moveX;
	public bool isGrounded = false;

	// Update is called once per frame
	void Update () {
		PlayerMove();
		PlayerRaycast ();
	}

	void PlayerMove() {
		// Controls
		moveX = Input.GetAxis("Horizontal");


		// Jumping
		if (Input.GetButtonDown ("Jump") && isGrounded) {
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
		isGrounded = false;
	}

	void OnCollisionEnter2D (Collision2D col) {
		Debug.Log ("Player has collided with " + col.collider.name);
	}

	void PlayerRaycast () {
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down);
		if (hit != null && hit.collider != null && hit.distance < 1.5f && hit.collider.tag == "Enemy") { // Hitting enemy from above
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 500); // Player jumps
			//hit.collider.gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 200);
			//hit.collider.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			//hit.collider.gameObject.GetComponent<EnemyMove> ().enabled = false;
			//Destroy (hit.collider.gameObject);	// Kill enemy
			hit.collider.gameObject.GetComponent<Animator> ().SetBool ("isDying", true);
			hit.collider.gameObject.GetComponent<EnemyMove> ().enabled = false;	

		}
		if (hit != null && hit.collider != null && hit.distance < 1.5f && hit.collider.tag != "Enemy") {
			isGrounded = true;

		}
	}

}

