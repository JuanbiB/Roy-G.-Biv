﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideSpikeSpawn : MonoBehaviour {
    public float spawnrate = 5;
    private float spawntime = 0;
    public GameObject SlideSpikePrefab;
	
	// Update is called once per frame
	void Update () {

        if (spawntime + 1 / spawnrate < Time.time)
        {
            spawntime = Time.time;
            Vector3 spawnPosition = transform.position;
            Instantiate(SlideSpikePrefab, spawnPosition, Quaternion.identity);
            
        }
    }
}
