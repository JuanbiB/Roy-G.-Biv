using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class will be implemented in the spike shooters to shoot out the spikes*/
public class SlideSpikeSpawn : MonoBehaviour {

    public float spawnrate = 5;
    private float spawntime = 0;
    public GameObject SlideSpikePrefab;
	
    /*update function shoots spikes at a certain spawn rate. Taken from
     * Benno's ShootEmUp code from Lab 1 of class*/
	void Update () {

        if (spawntime + 1 / spawnrate < Time.time)
        {
            spawntime = Time.time;
            Vector3 spawnPosition = transform.position;
            Instantiate(SlideSpikePrefab, spawnPosition, Quaternion.identity);
            
        }
    }
}
