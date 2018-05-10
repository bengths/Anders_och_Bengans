using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStats : MonoBehaviour {

	// Delegates
	public delegate void ObjectDelegateInt(int a);

	public static event ObjectDelegateInt OnPlayerScored;
	public static event ObjectDelegateInt OnPlayerHurt;
	public static event ObjectDelegateInt OnPlayerHeal;



	// Available object types
	public enum objectType {collectable, extraLife, healing, hurting};
	public objectType type;

	public int score;
	public int healPoints;
	public int hurtPoints;

	//public AudioSource objectSound;

	void OnTriggerEnter2D (Collider2D trig)
	{
		if (trig.gameObject.tag == "Player") {
			//objectSound.Play ();
			switch (type) {
			case objectType.collectable:
				OnPlayerScored (score);
				Destroy (this.gameObject);
				break;
			case objectType.extraLife:
				Debug.Log ("Player gained an extra life!");
				break;
			case objectType.healing:
				OnPlayerHeal (healPoints);
				break;
			case objectType.hurting:
				OnPlayerHurt (hurtPoints);
				break;
			}
		}
	}
}
