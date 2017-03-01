using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovePlatform : MonoBehaviour {

    // Constants to control the movement of the platform 
    // They don't actually work out to perfectly mimic distance and speed but they are close
    public float MoveDistance;
    public float MoveSpeed;

	Vector3 startPos;
	Vector3 endPos;

	void Start(){
		startPos = new Vector3 (transform.position.x - MoveDistance, transform.position.y, transform.position.z);
		endPos = new Vector3 (transform.position.x + MoveDistance, transform.position.y, transform.position.z);
	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (startPos, endPos, (Mathf.Sin(MoveSpeed * Time.time) + 1.0f) / 2.0f);
	}
}
