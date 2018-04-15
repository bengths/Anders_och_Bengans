using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapCursor : MonoBehaviour {

	private Vector2 cursorPos;
	private Vector2 oldCursorPos;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		oldCursorPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
	}
	
	// Update is called once per frame
	void Update () {
		cursorPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = cursorPos;

		// Check direction of moving cursor
		if ((cursorPos.x - oldCursorPos.x) < 0.0f)
			gameObject.GetComponent<SpriteRenderer> ().flipX = true;
		else if ((cursorPos.x - oldCursorPos.x) > 0.0f)
			gameObject.GetComponent<SpriteRenderer> ().flipX = false;
		
		oldCursorPos = cursorPos;

	}

	void OnDisable() {
		Cursor.visible = true;
	}
}
