using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buzzsaw : MonoBehaviour {
	public float rotationspeed=5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame--rotates the saw
	void Update () {
		transform.rotation *= Quaternion.AngleAxis (rotationspeed * Time.deltaTime, Vector3.forward);
	}
}
