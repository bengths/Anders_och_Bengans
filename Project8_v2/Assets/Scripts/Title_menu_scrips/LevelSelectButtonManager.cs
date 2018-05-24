using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButtonManager : MonoBehaviour {

	static public void LoadScene(string newSceneName) {
		SceneManager.LoadScene(newSceneName);
	}
}
