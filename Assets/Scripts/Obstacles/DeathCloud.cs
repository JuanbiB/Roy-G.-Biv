using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroys player if they come in contact
public class DeathCloud : MonoBehaviour {

	BoxCollider2D box_collider;

	// Use this for initialization
	void Start () {
		box_collider = GetComponent<BoxCollider2D> ();
		box_collider.isTrigger = true;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			// death to the player
			other.GetComponent<Player>().CloudDie();
		}
	}
}
