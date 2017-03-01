using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformerController))]
public class PlatformerInputModule : MonoBehaviour
{
    PlatformerController controller;
	public bool inDisplay;

    void Start()
    {
        controller = GetComponent<PlatformerController>();

    }

    void FixedUpdate()
    {
		if (inDisplay) {
			return;
		}

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.magnitude > 1) {
            input.Normalize();
        }
        controller.input = input;
		controller.inputJump = Input.GetButtonDown("Jump");

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            controller.RedMode();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            controller.YellowMode();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            controller.BlueMode();
        }
    }

	void OnTriggerEnter2D(Collider2D other){
        if (other.name == "juanbi") {
			controller.jumpVelocity = 25;
			Destroy (other.gameObject);
		}
			
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.SetParent(other.transform);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.SetParent(null);
        }
    }
		
}
