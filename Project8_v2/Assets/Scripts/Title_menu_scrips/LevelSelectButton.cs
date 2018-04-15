using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectButton : MonoBehaviour {

	public GameObject namePanel;

	void OnStart() {
		namePanel.SetActive (false);
	}


	void OnMouseEnter() {
		namePanel.SetActive (true);
	}

	void OnMouseExit() {
		namePanel.SetActive (false);
	}


}
