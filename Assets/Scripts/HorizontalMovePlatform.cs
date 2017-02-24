using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovePlatform : MonoBehaviour {

    // Constants to control the movement of the platform 
    // They don't actually work out to perfectly mimic distance and speed but they are close
    public float MoveDistance;
    public float MoveSpeed;
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x + Mathf.Sin(Time.time *MoveSpeed) * MoveDistance, transform.position.y, 0);
    }
}
