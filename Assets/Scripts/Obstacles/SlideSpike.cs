using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideSpike : MonoBehaviour {

    public float lifeTime = 5;
    Rigidbody2D rb2d;
    public float speed = 3;

    /* Initialize spike with speed and kill time*/
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        StartCoroutine(KillAfterSeconds(lifeTime));
    }

    /*Coroutine that kills the spike, adapted from Benno's ShootEmUp
     * lab example*/
    IEnumerator KillAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
