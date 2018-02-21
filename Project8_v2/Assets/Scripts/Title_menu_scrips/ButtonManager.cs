using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public GameObject dialogueCanvas;

	public void LoadSceneButton(string newSceneName) {
		SceneManager.LoadScene(newSceneName);
	}

	public void NewGameButton() {
		if (PlayerPrefs.HasKey ("hasOngoingGame")) {
			// Display dialogue box "Would you really like to delete your old save?"
			Debug.Log ("There is already a saved game. Would you like to replace it?");
			dialogueCanvas.SetActive(true);
		} else {
			startNewGame ();
		}
	}

	public void LoadGameButton() {
		if (PlayerPrefs.HasKey ("hasOngoingGame")) {
			SceneManager.LoadScene ("Level_Select");
		}
	}

	public void ExitGameButton() {
		Application.Quit();
	}

	// Dialogue canvas buttons
	public void ConfirmNewGameButton() {
		startNewGame ();
	}

	public void DismissNewGameButton() {
		dialogueCanvas.SetActive (false);
	}





	private void startNewGame() {
		PlayerPrefs.SetInt ("hasOngoingGame", 1); 	// Player has begun a new Game
		PlayerPrefs.SetInt ("Total_Score",0);
		PlayerPrefs.SetInt ("Lives", 3);
		PlayerPrefs.SetInt ("HighScore", 0);		// Flappy Fredrik HighScore
		//PlayerPrefs.SetInt ("UnlockLevelMagnus", 1);

		SceneManager.LoadScene ("Intro");
	}

}
