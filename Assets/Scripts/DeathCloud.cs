using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCloud : MonoBehaviour {

	BoxCollider2D box_collider;

	// Use this for initialization
	void Start () {
		box_collider = GetComponent<BoxCollider2D> ();
		box_collider.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			// death to the player
			Debug.Log("yoo");
			other.GetComponent<Player>().Die();
		}
	}
}
