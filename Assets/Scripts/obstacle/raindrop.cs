using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raindrop : MonoBehaviour {
	public float speed=5;
	public float lifeTime=5;

	// Use this for initialization
	void Start () {
		StartCoroutine (KillAfterSeconds (lifeTime));	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.down * speed * Time.deltaTime;
	}

	IEnumerator KillAfterSeconds(float seconds){
		yield return new WaitForSeconds(seconds); 
		Destroy(gameObject);
	}
}
