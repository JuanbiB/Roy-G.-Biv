using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickup : Pickup {

    // All color pickups need a reference to GameManager and Player
    public PlatformerController Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformerController>();
    }

    // All color pickups will grow/shrink
    void Update()
    {
        float x = 1 + 0.2f * Mathf.Sin(Time.time);
        float y = 1 + 0.1f * Mathf.Cos(Time.time);
        transform.localScale = new Vector3(x, y, 1);
    }
}
