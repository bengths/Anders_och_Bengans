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
	public float typeSpeed;

	private bool isTyping;
	private bool doneTyping;
	private bool canSwitchPanel;

	// Use this for initialization
	void Start () {
		if (textFile != null) {
			textLines = (textFile.text.Split ('\n'));
			displayTime = new float[textLines.Length];
			isTextboxEnabled = new int[textLines.Length];

			// Initialize displayTime
			for (int i = 0; i < textLines.Length-1; i++) {
				string[] tmpStr = (textLines [i].Split ('\t'));
				Debug.Log (tmpStr[0]);

				displayTime [i] = float.Parse(tmpStr [0]);
				isTextboxEnabled [i] = int.Parse (tmpStr [1]);
				textLines [i] = tmpStr [2];
			}

		}

		if (endAtLine == 0) {
			endAtLine = textLines.Length - 1;
		}

		// Make textbox invisible at start
		DisableTextBox ();
		isTyping = false;
		doneTyping = false;
		canSwitchPanel = true;

	}

	void Update()
	{
		if (canSwitchPanel) {
			canSwitchPanel = false;

			if (currentLine > endAtLine) {
				DisableTextBox ();
				return;
			}

			if (isTextboxEnabled [currentLine] == 0) {
				DisableTextBox ();
				StartCoroutine (IncrementLines ());
			}
			if (isTextboxEnabled [currentLine] == 1) {
				if (!isTyping && !doneTyping) {
					EnableTextBox ();
				}
				StartCoroutine (IncrementLines ());

			}
		}


	}

	private IEnumerator IncrementLines () {
		yield return new WaitForSeconds (displayTime [currentLine]);
		currentLine++;
		doneTyping = false;
		canSwitchPanel = true;
	}

	private IEnumerator AutoType (string lineOfText) {
		theText.text = "";
		isTyping = true;
		for(int letter = 0; letter < lineOfText.Length; letter++) {
			theText.text += lineOfText [letter];
			yield return new WaitForSeconds (typeSpeed);
		}
		theText.text = lineOfText;
		isTyping = false;
		doneTyping = true;
	}


	public void EnableTextBox() {
		textBox.SetActive (true);
		theText.text = textLines [currentLine];
		StartCoroutine (AutoType(textLines[currentLine]));
	}

	public void DisableTextBox() {
		textBox.SetActive (false);
	}

}
