using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneDialogue : MonoBehaviour {

	public GameObject textBox;

	public Text theText;

	public TextAsset textFile;
	public float[] displayTime;		// Time for being displayed
	public int[] isTextboxEnabled; 	// 1 == true, 0 == false
	public string[] textLines;		// Lines to be displayed

	public int currentLine;
	public int endAtLine;

	// Use this for initialization
	void Start () {
		if (textFile != null) {
			textLines = (textFile.text.Split ('\n'));
			displayTime = new float[textLines.Length];
			// Initialize displayTime
			for (int i = 0; i < textLines.Length; i++) {
				string[] tmpStr = (textLines [i].Split ('\t'));

				displayTime [i] = float.Parse (tmpStr [0]);
				isTextboxEnabled [i] = int.Parse (tmpStr [1]);
				textLines [i] = tmpStr [2];
			}

		}

		if (endAtLine == 0) {
			endAtLine = textLines.Length - 1;
		}

		// Make textbox invisible at start
		DisableTextBox ();
	}

	void Update()
	{

		theText.text = textLines [currentLine];

		// Increment after displayTime seconds
		if (Input.GetKeyDown (KeyCode.S)) {
			currentLine++;
		}

		if (isTextboxEnabled) {
			DisableTextBox ();
		}
		if (textLines [currentLine].Equals ("<ENABLE>")) {
			EnableTextBox ();
		}
			



	}

	public void EnableTextBox() {
		textBox.SetActive (true);
	}

	public void DisableTextBox() {
		textBox.SetActive (false);
	}

}
