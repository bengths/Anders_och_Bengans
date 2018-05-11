using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

	public float speed = 5 * 3.1415926535f;
	public GameObject impactParticles;
	private float horzDir = 0.0f;


	public void setHorz_dir(float x)
	{
		horzDir = x;
		this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(horzDir * speed, 0.0f);
	}


	void OnTriggerEnter2D(Collider2D trig)
	{
		if (trig.gameObject.tag == "Enemy" || trig.gameObject.tag == "ground") {
			GameObject impactPrtcls = Instantiate (impactParticles, this.GetComponent<Transform> ());
			GetComponent<Rigidbody2D> ().velocity = new Vector3 (0.0f, 0.0f, 0.0f);
			GetComponent<SpriteRenderer> ().enabled = false;
			GetComponent<BoxCollider2D> ().enabled = false;
			StartCoroutine ("waitForDestroyCo");
		}
			
	}

	IEnumerator waitForDestroyCo() {
		
		yield return new WaitForSeconds (2.0f);
		Destroy(this.gameObject);
	}
}
