using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script is used to change the color on the menu text
public class MenuColorSwitcher : MonoBehaviour {

	Text royText;
	float timeCounter;
	Color [] colors;
	int counter;

	// Use this for initialization
	void Start () {
		royText = GetComponent<Text> ();
		timeCounter = 0;
		royText.color = Color.red;

		colors = new Color[3]{Color.red, Color.yellow, Color.blue};
		counter = 0;
	}
	
	void Update(){
		timeCounter += Time.deltaTime;

		if (timeCounter > .5f) {
			changeColor ();
			timeCounter = 0;
		}
	}

	void changeColor(){
		counter++;
		Color newColor = colors [counter % 3];
		royText.color = newColor;
	}
}
