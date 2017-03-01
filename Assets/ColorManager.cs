using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ColorManager : MonoBehaviour {

	enum Colors { Red, Blue, Yellow} 
	Colors current = Colors.Red;
	float counter;
	Text text;

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<Text> ();
		counter = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;

		if (counter > .5f) {
			if (current == Colors.Red) {
				current = Colors.Blue;
				text.color = Color.blue;
			} else if (current == Colors.Blue) {
				current = Colors.Yellow;
				text.color = Color.yellow;
			} else if (current == Colors.Yellow) {
				current = Colors.Red;
				text.color = Color.red;
			}

			counter = 0f;
		}
	}
}
