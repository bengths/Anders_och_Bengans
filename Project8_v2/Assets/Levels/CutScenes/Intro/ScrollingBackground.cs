using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

	public float backgroundSize;
	public float paralaxSpeed;
	public bool autoScroll;

	private Transform cameraTransform;
	private Transform[] layers;
	private float viewZone = 8;
	private int leftIndex;
	private int rightIndex;

	private void Start () {
		// Get transform of camera
		cameraTransform = Camera.main.transform;

		// Get transform of children
		layers = new Transform[transform.childCount];	
		for(int i = 0; i < transform.childCount; i++) {
			layers[i] = transform.GetChild(i);
		}

		// Initialize indicies
		leftIndex = 0;
		rightIndex = layers.Length-1;
	}

	private void Update() {
		if (autoScroll) {
			transform.position += Vector3.right * (Time.deltaTime * paralaxSpeed);
		}

		if (cameraTransform.position.x < (layers [leftIndex].transform.position.x + viewZone))
			ScrollLeft ();
		
		if (cameraTransform.position.x > (layers [rightIndex].transform.position.x - viewZone))
			ScrollRight();

	}

		
	private void ScrollLeft() {
		int lastRight = rightIndex;
		layers [rightIndex].position = new Vector3(layers [leftIndex].position.x - backgroundSize, layers[leftIndex].position.y, layers[leftIndex].position.z);
		leftIndex = rightIndex;
		rightIndex--;
		if (rightIndex < 0) {
			rightIndex = layers.Length - 1;
		}
	}

	private void ScrollRight() {
		int lastLeft = leftIndex;
		layers [leftIndex].position = new Vector3(layers [rightIndex].position.x + backgroundSize, layers[rightIndex].position.y, layers[rightIndex].position.z);//Vector3.right * (layers [rightIndex].position.x + backgroundSize);
		rightIndex = leftIndex;
		leftIndex++;
		if (leftIndex == layers.Length) {
			leftIndex = 0;
		}
	}

}
