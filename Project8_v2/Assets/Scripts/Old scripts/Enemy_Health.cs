using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour {

	private float timeOfDeath;
	private int index = -1;

	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y < -7) {
			Destroy (gameObject);
		}

		if (gameObject.GetComponent<Animator> ().GetBool("isDying")) {
			index += 1;
			if (index == 0) {
				timeOfDeath = Time.time;
			}

			if(Time.time - timeOfDeath > 3.0f) { // Delay before destroying enemy animation
				Destroy (gameObject);
			}
		}
	}
}
