﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickup : MonoBehaviour {

    public AudioClip LevelUpSound;

    // All color pickups will oscillate in size
    void Update()
    {
        float x = 10 + 2f * Mathf.Sin(Time.time);
        float y = 10 + 1f * Mathf.Cos(.7f * Time.time);
        transform.localScale = new Vector3(x, y, 1);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(LevelUpSound, transform.position);
            MyFunction();
            Destroy(gameObject);
        }
    }

    // Function of the pickup--should be overridden
    public virtual void MyFunction() { }
}
