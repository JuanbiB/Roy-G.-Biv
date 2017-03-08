using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikesTrigger : MonoBehaviour
{

    // Falling spikes lifetime
    public float lifeTime = 2;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ReturnToStart(float seconds, GameObject spikes, Vector3 origSpikesPos)
    {
        yield return new WaitForSeconds(seconds); // this halts the functions execution for x seconds. Can only be used in coroutines.
        Rigidbody2D spikesrb = GetComponentInParent<Rigidbody2D>();
        spikesrb.velocity = Vector3.zero;
        spikesrb.isKinematic = true;
        spikes.transform.position = origSpikesPos;
    }

    public void Fall()
    {
        Vector3 spikesStartPos = transform.parent.transform.position;
        GameObject spikes = transform.parent.gameObject;
        Rigidbody2D spikesrb = GetComponentInParent<Rigidbody2D>();
        spikesrb.isKinematic = false;
        StartCoroutine(ReturnToStart(lifeTime, spikes, spikesStartPos));
    }
}
