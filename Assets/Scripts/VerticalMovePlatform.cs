using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovePlatform : MonoBehaviour {

	public float MoveDistance;
	public float MoveSpeed;

	Vector3 startPos;
	Vector3 endPos;

	void Start(){
		startPos = new Vector3 (transform.position.x, transform.position.y - MoveDistance, transform.position.z);
		endPos = new Vector3 (transform.position.x, transform.position.y + MoveDistance, transform.position.z);
	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (startPos, endPos, (Mathf.Sin(MoveSpeed * Time.time) + 1.0f) / 2.0f);
	}
}
