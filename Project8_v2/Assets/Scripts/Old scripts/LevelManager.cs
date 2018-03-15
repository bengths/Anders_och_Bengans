using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject currentCheckpoint;
	private Player_Move_Prototype player;
	public GameObject deathParticles;
	public GameObject spawnParticles;
	public float respawnDelay;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player_Move_Prototype> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RespawnPlayer () {
		StartCoroutine ("RespawnPlayerCo");
	}

	public IEnumerator RespawnPlayerCo () {
		Instantiate (deathParticles, player.transform.position, player.transform.rotation);
		player.enabled = false;
		player.GetComponent<Renderer>().enabled = false;

		yield return new WaitForSeconds (respawnDelay);
		Debug.Log ("Player respawn");
		player.transform.position = currentCheckpoint.transform.position;
		Instantiate (spawnParticles, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
		player.enabled = true;
		player.GetComponent<Renderer>().enabled = true;
	}

}
