using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public delegate void PlayerDelegate();
	public static event PlayerDelegate OnPlayerDied;
	public static event PlayerDelegate OnPlayerScored;

	public float tapForce = 10;
	public float tiltSmooth = 5;
	public Vector3 startPos;

	public AudioSource tapAudio;
	public AudioSource scoreAudio;
	public AudioSource dieAudio;


	Rigidbody2D rigidbody;
	Quaternion downRotation;
	Quaternion forwardRotation;

	GameManagerFF game;

	void Start() {
		rigidbody = GetComponent<Rigidbody2D> ();
		downRotation = Quaternion.Euler (0, 0, -90);
		forwardRotation = Quaternion.Euler (0, 0, 35);
		game = GameManagerFF.Instance;
		rigidbody.simulated = false;
	}

	void OnEnable() {
		GameManagerFF.OnGameStarted += OnGameStarted;
		GameManagerFF.OnGameOverConfirmed += OnGameOverConfirmed;
	}

	void OnDisable() {
		GameManagerFF.OnGameStarted -= OnGameStarted;
		GameManagerFF.OnGameOverConfirmed -= OnGameOverConfirmed;
	}

	void OnGameStarted() { // Reset physics
		rigidbody.velocity = Vector3.zero;
		rigidbody.simulated = true;

	}

	void OnGameOverConfirmed ()	{ // Reset position and rotation
		transform.localPosition = startPos;
		transform.rotation = Quaternion.identity;
	}




	void Update() {
		if (game.GameOver) {
			return;
		}

		if (Input.GetMouseButtonDown(0)) {
			tapAudio.Play ();
			transform.rotation = forwardRotation;
			rigidbody.velocity = Vector3.zero;
			rigidbody.AddForce (Vector2.up * tapForce, ForceMode2D.Force); 
		}

		transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "ScoreZone") {
			// Register a score Event
			OnPlayerScored(); // Event sent to GameManagerFF;
			// Play a sound
			scoreAudio.Play();
		}

		if (col.gameObject.tag == "DeadZone") {
			rigidbody.simulated = false;
			// register a dead event
			OnPlayerDied(); // Event sent to GameManagerFF;
			// Play a sound
			dieAudio.Play();

		}
	}
}
