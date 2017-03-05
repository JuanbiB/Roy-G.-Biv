using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public float MoveDistance;
    public float MoveSpeed;

    public int numOfPoints;
    public Transform[] points;
    public Transform current;

    Vector3 startPos;
    Vector3 endPos;

    void Start()
    {
        points = new Transform[numOfPoints];
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == endPos)
        {
            startPos = endPos;
            endPos = endPos;
        }
        transform.position = Vector3.Lerp(startPos, endPos, (Mathf.Sin(MoveSpeed * Time.time) + 1.0f) / 2.0f);
    }
}
