using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script is to be added to any falling spike triggers. The script
 * returns the spike to its original position after a set lifetime*/
public class FallingSpikesTrigger : MonoBehaviour
{

    // Falling spikes lifetime
    public float lifeTime = 2;

    /*method is called when the spike fall is triggered in the player script.
     * The spike falls and then the return to start coroutine is started.*/
    public void Fall()
    {
        //get original properties for returning spike to start
        Vector3 spikesStartPos = transform.parent.transform.position;
        GameObject spikes = transform.parent.gameObject;
        Rigidbody2D spikesrb = GetComponentInParent<Rigidbody2D>();

        //make spike fall and return to start
        spikesrb.isKinematic = false;
        StartCoroutine(ReturnToStart(lifeTime, spikes, spikesStartPos));
    }

    IEnumerator ReturnToStart(float seconds, GameObject spikes, Vector3 origSpikesPos)
    {
        //after lifetime set the spike back to kinematic mode and return spike to original position
        yield return new WaitForSeconds(seconds); 
        Rigidbody2D spikesrb = GetComponentInParent<Rigidbody2D>();
        spikesrb.velocity = Vector3.zero;
        spikesrb.isKinematic = true;
        spikes.transform.position = origSpikesPos;
    }
}
