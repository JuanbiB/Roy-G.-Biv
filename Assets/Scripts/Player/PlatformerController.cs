﻿using System.Collections;
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

	PlatformerInputModule inputModule;

	bool grounded;
	Rigidbody2D rb2d;
	public SpriteRenderer sr;
	Animator anim;

	int facing = 1;

	Transform myTrans;
	GameObject[] groundTags;
    public LayerMask playerMask;
    public bool isGrounded = false;

	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
        myTrans = this.transform;
		groundTags = GameObject.FindGameObjectsWithTag("ground_tag");
		inputModule = gameObject.GetComponent<PlatformerInputModule> ();

		anim = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer> ();
	}

	void Update ()
	{
		if (inputModule.inDisplay) {
			return;
		}
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

		if (Player.instance.playerMode == Player.Mode.Blue && newVelocity.y < -20) {
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
		//if (lastJumpTime == Time.time) {
		//	anim.SetTrigger ("jump");
		//}
	}
}
