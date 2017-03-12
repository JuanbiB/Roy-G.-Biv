using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class makes a moving platform between defined points in space
public class MovingPlatform : MonoBehaviour {

    // Object representing the object that will move
    public GameObject platform;

    public float MoveSpeed;
    
    // Points which the object will move to
    public Transform[] points;

    // Current point to move towards and the index of that point in the points array
    Transform endPoint;
    int currentPointIndex;

    void Start()
    {
        endPoint = points[currentPointIndex];
    }
    

    // Update is called once per frame--moves the platform between the two points
    void Update()
    {
        if(points.Length <= 1)
        {
            return;
        }
        if (platform.transform.position == endPoint.position)
        {
            currentPointIndex = (currentPointIndex + 1) % points.Length;
            endPoint = points[currentPointIndex];
        }
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, endPoint.position, Time.deltaTime * MoveSpeed);
    }   
}
