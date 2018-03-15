using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour {

	public float speed;
	public float xDespawn;
	public float xRespawn;
	private float xDir;

	// Use this for initialization
	void Start () {
		xDir = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		xDir -= Time.deltaTime * speed;
		transform.position = new Vector3 (xDir, transform.position.y, transform.position.z);

		// Apply periodic boundary conditions
		if (transform.position.x < xDespawn) {
			transform.position = new Vector3 (xRespawn, transform.position.y, transform.position.z);
			xDir = transform.position.x;
		}

	}
}
