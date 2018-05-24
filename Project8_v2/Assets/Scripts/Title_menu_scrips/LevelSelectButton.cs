using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour {

    // Level Select Button Delegates
    public delegate void LevelSelectButtonDelegate(string str, string strr);
    public static event LevelSelectButtonDelegate OnClickedButton;
    public GameObject namePanel;
    public Text levelName;
    public Text sceneName;

	void OnStart() {
		namePanel.SetActive (false);
	}


	void OnMouseEnter() {
		namePanel.SetActive (true);
	}

	void OnMouseExit() {
		namePanel.SetActive (false);
	}

    public void UpdateLevelName()
    {
        OnClickedButton(levelName.text,sceneName.text);    // Send event with the level name
    }

}
