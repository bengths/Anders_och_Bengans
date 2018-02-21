using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectButton : MonoBehaviour {

	public GameObject namePanel;



	void OnMouseOver() {
		Debug.Log ("Mouse on Button");
		namePanel.SetActive (true);
	}

	void OnMouseExit() {
		Debug.Log ("Mouse left Button");
		namePanel.SetActive (false);
	}


}
