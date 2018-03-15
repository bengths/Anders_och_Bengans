using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour {

	public LevelManager levelManager;

	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
	}

	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.name == "Player") {
			levelManager.currentCheckpoint = gameObject;
			Debug.Log("Activated Checkpoint " + gameObject.GetComponent<Transform>().position);
		}
	}

}
