using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlatformerController : MonoBehaviour
{
	public Vector2 input;
	public bool inputJump;

	public float speed = 5;
	public float jumpVelocity = 15;
	public float gravity = 40;
	public float groundingTolerance = .1f;
	public float jumpingTolerance = .1f;

	public LayerMask groundLayers;

	bool grounded;
	Rigidbody2D rb2d;
	SpriteRenderer sr;
	Animator anim;

	float lostGroundingTime;
	float lastJumpTime;
	float lastInputJump;
	int facing = 1;

	Transform myTrans;
	GameObject[] groundTags;
    public LayerMask playerMask;
    public bool isGrounded = false;

    // Different modes for the player
    public enum Mode { White, Red, Yellow, Blue}

    // Current mode of the player
    public Mode playerMode;

	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
        myTrans = this.transform;
		groundTags = GameObject.FindGameObjectsWithTag("ground_tag");

		anim = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer> ();
        playerMode = Mode.White;
	}

	void Update ()
	{
		foreach (GameObject gt in groundTags) {
			isGrounded = Physics2D.Linecast(myTrans.position, gt.transform.position, playerMask);
			if (isGrounded)
				break;	
		}
        
        // grounded = CheckGrounded ();
        ApplyHorizontalInput ();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

		UpdateAnimations ();
	}

	void ApplyHorizontalInput ()
	{
		Vector2 newVelocity = rb2d.velocity;
		newVelocity.x = input.x * speed;
		newVelocity.y += -gravity * Time.deltaTime;

		if (playerMode == Mode.Blue && newVelocity.y < -20) {
			rb2d.velocity = new Vector2 (newVelocity.x, rb2d.velocity.y);
		} else {
			rb2d.velocity = newVelocity;
		}

	}

	void Jump ()
	{
        if (isGrounded)
        {
            rb2d.velocity += jumpVelocity * Vector2.up;
        }
	}

        
	void UpdateAnimations ()
	{ 
		if (rb2d.velocity.x > 0 && facing == -1) {
			facing = 1;
		} else if(rb2d.velocity.x < 0 && facing == 1) {
			facing = -1;
		}
		sr.flipX = facing == -1 ;

        //CHANGED isGROUNDED and FIXED ANIMATIONS on PLATFORMS
		anim.SetBool ("grounded", isGrounded);
		anim.SetFloat ("speed", Mathf.Abs(rb2d.velocity.x));
		if (lastJumpTime == Time.time) {
			anim.SetTrigger ("jump");
		}
	}
		

    // Reset defaults to avoid retaining characteristics from other modes.
    void ResetAttributes()
    {
        jumpVelocity = 15;
        speed = 5;
		gravity = 40;
    }


    // Change to red mode, where the player can jump higher.
    public void RedMode()
    {
        ResetAttributes();
        playerMode = Mode.Red;
        sr.color = Color.red;
        jumpVelocity = 25;
    }

    // Change to yellow mode, where the player can run faster.
    public void YellowMode()
    {
        ResetAttributes();
        playerMode = Mode.Yellow;
        sr.color = Color.yellow;
        speed = 15;
    }

    //Change to blue mode, where the player glides.
    public void BlueMode()
    {
        ResetAttributes();
        playerMode = Mode.Blue;
        sr.color = Color.blue;
		gravity = 15;
		jumpVelocity = 10;
    }
    
}
