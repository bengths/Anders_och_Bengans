using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void LoadSceneButton(string newSceneName) {
		SceneManager.LoadScene(newSceneName);
	}

	public void ExitGameButton() {
		Application.Quit();
	}
}
