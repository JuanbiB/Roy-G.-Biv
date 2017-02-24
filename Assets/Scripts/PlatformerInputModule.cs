using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformerController))]
public class PlatformerInputModule : MonoBehaviour
{
    PlatformerController controller;

    void Start()
    {
        controller = GetComponent<PlatformerController>();
    }

    void FixedUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.magnitude > 1) {
            input.Normalize();
        }
        controller.input = input;
		controller.inputJump = Input.GetButtonDown("Jump");

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			controller.playerMode = PlatformerController.Mode.Red;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			controller.playerMode = PlatformerController.Mode.Yellow;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			controller.playerMode = PlatformerController.Mode.Blue;
		}
		controller.CheckMode ();
    }

	void OnTriggerEnter2D(Collider2D other){
        if (other.name == "juanbi") {
			controller.jumpVelocity = 25;
			Destroy (other.gameObject);
		}

        // Was using this code to play around with different ways for the player to move with a moving platform
        /**
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.SetParent(other.transform);
        }*/
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

    // Was using this code to play around with different ways for the player to move with a moving platform
    /**
    void OnTriggerExit2D(Collider2D other)
    {
        transform.SetParent(null);
    }

    void onTriggerStay2D(Collider2D other)
    {
        Debug.Log("stay");
        if (other.gameObject.tag == "MovingPlatform") {
            Debug.Log("hi");
            transform.SetParent(other.transform);
        }
    }*/
}
