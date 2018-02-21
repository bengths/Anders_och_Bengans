using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour {

	public enum cutSceneExitType{Automatic,Manual};
	public cutSceneExitType type;

	public string nextScene;
	public int playTime;


	// Use this for initialization
	void Start () {
		switch (type) {
		case cutSceneExitType.Automatic:
			// Start an automatic countdown until move to new scene
			StartCoroutine("StartAutomaticCountdownCo");
			break;
		case cutSceneExitType.Manual:
			// Wait for player to actively exit cutscene
			break;
		}
	}


	IEnumerator StartAutomaticCountdownCo () {
		yield return new WaitForSeconds (playTime);
		SceneManager.LoadScene (nextScene);
	}

}
