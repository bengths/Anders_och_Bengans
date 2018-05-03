using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour {
/*
	public Texture2D fadeTexture;
	public float fadeSpeed = 0.0f;

	private int drawDepth = -1000;
	private float alpha = 1.0f;
*/

	public AudioSource thunderAudio;
	public Texture2D fadeTexture;
	public float thunderRate = 4.0f;
	public float fadeSpeed = 1.0f;

	private int ii;
	private float alpha = 0.0f;
	private int drawDepth = -1000;
	private int fadeDir = 1;
	private bool ongoingStrike = false;
	private float lastStrike;


	void Start() {
		lastStrike = 0.0f;
	}
	/*
	void Update(){
		if ((Time.time - lastStrike) > (thunderRate + Random.Range(0.0f,3.0f))) 
		{
			StartCoroutine ("thunderStrikeCo");
			lastStrike = Time.time;
		}
	}
*/
	void OnGUI() {
		if ((Time.time - lastStrike) > (thunderRate + Random.Range (0.0f, 5.0f))) {
			ongoingStrike = true;
			fadeDir = 1;
			lastStrike = Time.time;
		}

		if (ongoingStrike) 
		{
			if (alpha == 1.0f) { 	// Start fade out
				fadeDir = -1;
				StartCoroutine("thunderStrikeCo");
			} else if (alpha == 0.0f && fadeDir == -1) { // Start fade in
				ongoingStrike = false;
			}

			alpha += fadeDir * fadeSpeed * Time.deltaTime;
			alpha = Mathf.Clamp01 (alpha);
			GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
			GUI.depth = drawDepth;
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeTexture);
		}
	}

	IEnumerator thunderStrikeCo() {
		yield return new WaitForSeconds (Random.Range (0.2f, 3.5f));
		gameObject.GetComponent<AudioSource> ().pitch = Random.Range (0.4f, 1.2f);
		gameObject.GetComponent<AudioSource> ().volume = Random.Range (0.8f, 1.0f);
		gameObject.GetComponent<AudioSource> ().Play ();
		//thunderAudio.Play ();
	}

}
