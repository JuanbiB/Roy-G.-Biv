using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickup : Pickup {

    // All color pickups will oscillate in size
    void Update()
    {
        float x = 10 + 2f * Mathf.Sin(Time.time);
        float y = 10 + 1f * Mathf.Cos(.7f * Time.time);
        transform.localScale = new Vector3(x, y, 1);
    }
}
