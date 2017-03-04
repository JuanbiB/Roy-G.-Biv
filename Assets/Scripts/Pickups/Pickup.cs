using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	public void OnCollision2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MyFunction();
            Destroy(gameObject);
        }
    }

    // Function of the pickup--should be overridden
    public virtual void MyFunction() { }
}
