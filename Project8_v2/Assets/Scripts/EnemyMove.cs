using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour {

	public int enemySpeed;
	public int xMoveDirection;

	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2 (xMoveDirection, 0));
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (xMoveDirection, 0) * enemySpeed;
		if (hit.distance < 1.2f) {
			Flip ();
			if (hit.collider.tag == "Player") {
				SceneManager.LoadScene ("Test_Scene");
				//Destroy (hit.collider.gameObject);
			}
		}

		// Enemy Direction
		if (xMoveDirection < 0.0f) {
			// Flip player
			GetComponent<SpriteRenderer> ().flipX = false;
		} else if (xMoveDirection > 0.0f) {
			GetComponent<SpriteRenderer> ().flipX = true;
		}

	}

	void Flip() {
		if (xMoveDirection > 0) {
			xMoveDirection = -1;
		} else {
			xMoveDirection = 1;
		}
	}
}
