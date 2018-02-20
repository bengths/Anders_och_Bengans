using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	// Delegates
	public delegate void CheckpointDelegate(GameObject checkpoint);

	public static event CheckpointDelegate setCheckpoint;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.name == "Player") {
			Debug.Log("Activated Checkpoint " + gameObject.GetComponent<Transform>().position);
			setCheckpoint (gameObject);
		}
	}
}
