using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Prototype : MonoBehaviour {

	public int playerSpeed = 10;
	//public int playerJumpPower = 1250;
	public float playerJumpHeight;
	private float moveX;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool isGrounded;

	private bool doubleJumped;

	// Checks if player is touching the solid ground layer
	//void FixedUpdate () {
	//	isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	//}


	// Update is called once per frame
	void Update () {
		PlayerMove();
		PlayerRaycast ();
	}

	void PlayerMove() {
		// Controls
		moveX = Input.GetAxis("Horizontal");


		// Jumping
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
		if (isGrounded) {
			doubleJumped = false;
		}

		if (Input.GetButtonDown ("Jump") && isGrounded) {
			Jump();
			GetComponent<Animator> ().SetBool ("isJumping", true);
		} 

		if (Input.GetButtonDown ("Jump") && !isGrounded && !doubleJumped) {
			Jump ();
			GetComponent<Animator> ().SetBool ("isJumping", true);
			doubleJumped = true;
		}


		if (gameObject.GetComponent<Rigidbody2D> ().velocity.y < 0) {
			// Player is falling
			GetComponent<Animator> ().SetBool ("isJumping", false);
			GetComponent<Animator> ().SetBool ("isFalling", true);
		} else if (isGrounded || doubleJumped) {
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
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, playerJumpHeight);
		//GetComponent<Rigidbody2D>().AddForce (Vector2.up * playerJumpPower);
		//isGrounded = false;
	}

	void OnCollisionEnter2D (Collision2D col) {
		//Debug.Log ("Player has collided with " + col.collider.name);
	}

	void PlayerRaycast () {
		//TODO FIX THIS AWFUL CODE AT SOME POINT!

		// Hitting from side
		RaycastHit2D rayRight = Physics2D.Raycast (transform.position, Vector2.right);
		if (rayRight != null && rayRight.collider != null && rayRight.distance < 1.5f && Input.GetButtonDown ("Fire1") && !GetComponent<SpriteRenderer> ().flipX) {
			if (rayRight.collider.tag == "Enemy") {
				rayRight.collider.gameObject.GetComponent<Animator> ().SetBool ("isDying", true);
				rayRight.collider.gameObject.GetComponent<EnemyMove> ().enabled = false;
			} else if (rayRight.collider.tag == "Breakable") {
				Destroy (rayRight.collider.gameObject);
			}
		}
		RaycastHit2D rayLeft = Physics2D.Raycast (transform.position, Vector2.left);
		if (rayLeft != null && rayLeft.collider != null && rayLeft.distance < 1.5f && Input.GetButtonDown ("Fire1") && GetComponent<SpriteRenderer> ().flipX) {
			if (rayLeft.collider.tag == "Enemy") {
				rayLeft.collider.gameObject.GetComponent<Animator> ().SetBool ("isDying", true);
				rayLeft.collider.gameObject.GetComponent<EnemyMove> ().enabled = false;
			} else if (rayLeft.collider.tag == "Breakable") {


				Destroy (rayLeft.collider.gameObject);
			}
		}



		// Hopping onto
		RaycastHit2D rayDown = Physics2D.Raycast (transform.position, Vector2.down);
		if (rayDown != null && rayDown.collider != null && rayDown.distance < 1.5f && rayDown.collider.tag == "Enemy") { // Hitting enemy from above
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 500); // Player jumps
			//rayDown.collider.gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 200);
			//rayDown.collider.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			//rayDown.collider.gameObject.GetComponent<EnemyMove> ().enabled = false;
			//Destroy (rayDown.collider.gameObject);	// Kill enemy
			rayDown.collider.gameObject.GetComponent<Animator> ().SetBool ("isDying", true);
			rayDown.collider.gameObject.GetComponent<EnemyMove> ().enabled = false;	

		}

		//if (rayDown != null && rayDown.collider != null && rayDown.distance < 1.2f && rayDown.collider.tag != "Enemy") {
		//	isGrounded = true;
		//}



	}


}

