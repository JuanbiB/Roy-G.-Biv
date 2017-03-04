using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("COLLISION");
            MyFunction();
            Destroy(gameObject);
        }
    }

    // Function of the pickup--should be overridden
    public virtual void MyFunction() { }
}
