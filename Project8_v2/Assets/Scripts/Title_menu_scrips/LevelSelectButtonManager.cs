﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButtonManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void LoadScene(string newSceneName) {
		SceneManager.LoadScene(newSceneName);
	}


}
