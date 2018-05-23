using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSelectScript : MonoBehaviour {

	// Characters in order
	public GameObject ProfileMagnus;
	public GameObject ProfileAnders;
	public GameObject ProfileAnton;
	public GameObject ProfileJohan;
	public GameObject ProfileJonas;
	public GameObject ProfileMarcus;
	public GameObject ProfileDick;

	public Text caption;

	private int characterNbr;


	void Awake() {
		// Enable all children
		Debug.Log("It's enabled");
		// Set character to default
		characterNbr = 2;
		UpdateProfile ();
	}

	public void OnLeftSwipe() {
		// Decrement character number
		characterNbr--;
		if (characterNbr < 1)
			characterNbr = 7;
		
		UpdateProfile ();
		Debug.Log (characterNbr);
		// Update UI
	}

	public void OnRightSwipe() {
		// Increment character number
		characterNbr++;
		if (characterNbr > 7)
			characterNbr = 1;

		UpdateProfile ();
		Debug.Log (characterNbr);
		// Update UI
	}

	void UpdateProfile() {
		switch (characterNbr) {
		case 1:
			caption.text = "Magnus";
			ProfileMagnus.SetActive (true);
			ProfileAnders.SetActive (false);
			ProfileAnton.SetActive (false);
			ProfileJohan.SetActive (false);
			ProfileJonas.SetActive (false);
			ProfileMarcus.SetActive (false);
			ProfileDick.SetActive (false);
			break;
		case 2:
			caption.text = "Anders";
			ProfileMagnus.SetActive (false);
			ProfileAnders.SetActive (true);
			ProfileAnton.SetActive (false);
			ProfileJohan.SetActive (false);
			ProfileJonas.SetActive (false);
			ProfileMarcus.SetActive (false);
			ProfileDick.SetActive (false);
			break;
		case 3:
			caption.text = "Anton";
			ProfileMagnus.SetActive (false);
			ProfileAnders.SetActive (false);
			ProfileAnton.SetActive (true);
			ProfileJohan.SetActive (false);
			ProfileJonas.SetActive (false);
			ProfileMarcus.SetActive (false);
			ProfileDick.SetActive (false);
			break;
		case 4:
			caption.text = "Johan";
			ProfileMagnus.SetActive (false);
			ProfileAnders.SetActive (false);
			ProfileAnton.SetActive (false);
			ProfileJohan.SetActive (true);
			ProfileJonas.SetActive (false);
			ProfileMarcus.SetActive (false);
			ProfileDick.SetActive (false);
			break;
		case 5:
			caption.text = "Jonas";
			ProfileMagnus.SetActive (false);
			ProfileAnders.SetActive (false);
			ProfileAnton.SetActive (false);
			ProfileJohan.SetActive (false);
			ProfileJonas.SetActive (true);
			ProfileMarcus.SetActive (false);
			ProfileDick.SetActive (false);
			break;
		case 6:
			caption.text = "Marcus";
			ProfileMagnus.SetActive (false);
			ProfileAnders.SetActive (false);
			ProfileAnton.SetActive (false);
			ProfileJohan.SetActive (false);
			ProfileJonas.SetActive (false);
			ProfileMarcus.SetActive (true);
			ProfileDick.SetActive (false);
			break;
		case 7:
			caption.text = "Dick";
			ProfileMagnus.SetActive (false);
			ProfileAnders.SetActive (false);
			ProfileAnton.SetActive (false);
			ProfileJohan.SetActive (false);
			ProfileJonas.SetActive (false);
			ProfileMarcus.SetActive (false);
			ProfileDick.SetActive (true);
			break;
		}

	}
}
