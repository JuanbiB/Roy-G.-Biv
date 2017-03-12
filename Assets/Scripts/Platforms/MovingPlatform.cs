using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class makes a moving platform between any two points in space
public class MovingPlatform : MonoBehaviour {

    public GameObject platform;

    public float MoveSpeed;
    
    public Transform[] points;
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
