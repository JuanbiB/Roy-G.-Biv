using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformerController))]
public class PlatformerInputModule : MonoBehaviour
{
    PlatformerController controller;
	public bool inDisplay;

	RaycastHit2D hitSideRay;
	public LayerMask playerMask;

    void Start()
    {
        controller = GetComponent<PlatformerController>();


    }

    void Update()
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
			hitSideRay = Physics2D.Linecast(transform.position, other.transform.position, playerMask);
			Vector3 hitSideNormal = hitSideRay.normal;
			hitSideNormal = hitSideRay.transform.TransformDirection(hitSideNormal);

			if (hitSideNormal == hitSideRay.transform.up)
			{
				Debug.Log("top");
				transform.SetParent(other.transform);
			} else if (hitSideNormal == hitSideRay.transform.right)
			{
				Debug.Log("right");
			} else if (hitSideNormal == -hitSideRay.transform.right)
			{
				Debug.Log("left");
			} else if (hitSideNormal == -hitSideRay.transform.up)
			{
				Debug.Log("bottom");
			}
		}
	
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.SetParent(null);
			controller.GetComponent<Animator>().SetBool("grounded", false);
        }
    }
		
}
