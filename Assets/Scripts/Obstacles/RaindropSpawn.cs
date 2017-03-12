using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raindropspawn : MonoBehaviour {
	public float spawnrate=5;
	public float spawnwidth=5;
	private float spawntime=0;
	public GameObject raindropPreFab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (spawntime + 1 / spawnrate < Time.time) {
			spawntime = Time.time;
			Vector3 spawnPosition = transform.position;
			spawnPosition += new Vector3(Random.Range(-spawnwidth, spawnwidth), 0, 0);
		
			Instantiate(raindropPreFab, spawnPosition, Quaternion.identity);
		}
	}
}
