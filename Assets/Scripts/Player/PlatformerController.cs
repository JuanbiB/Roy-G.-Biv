using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlatformerController : MonoBehaviour
{
	public Vector2 input;
	public bool inputJump;

    // jump properties
	public float speed = 5;
	public float jumpVelocity = 15;
	public float gravity = 40;

	public LayerMask groundLayers;
    
    // variables used for movement and jumping
	bool grounded;
    public bool glideMode = false;
    public bool isGrounded = false;
    int facing = 1;

    //Components to be used
    Rigidbody2D rb2d;
	public SpriteRenderer sr;
	Animator anim;
	Transform myTrans;
	GameObject[] groundTags;
    public LayerMask playerMask;

    // jump sounds
    public AudioClip JumpSound;

    void Start ()
	{
        // initialize components
		rb2d = GetComponent<Rigidbody2D> ();
        myTrans = transform;
		groundTags = GameObject.FindGameObjectsWithTag("ground_tag");

		anim = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer> ();
	}


    //Apply player input and update animation
	void Update ()
	{
		if (Player.instance.inDisplay) {
			return;
		}
		foreach (GameObject gt in groundTags) {
			isGrounded = Physics2D.Linecast(myTrans.position, gt.transform.position, playerMask);
			if (isGrounded)
				break;	
		}
        
        ApplyHorizontalInput ();

		if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

		UpdateAnimations ();
	}

    // move left or right
	void ApplyHorizontalInput ()
	{
		Vector2 newVelocity = rb2d.velocity;
		newVelocity.x = input.x * speed;
		newVelocity.y += -gravity * Time.deltaTime;

		if (glideMode && newVelocity.y < -10) {
			rb2d.velocity = new Vector2 (newVelocity.x, rb2d.velocity.y);
		} else {
			rb2d.velocity = newVelocity;
		}

	}

	void Jump ()
	{
        if (isGrounded)
        {
            AudioSource.PlayClipAtPoint(JumpSound, transform.position);
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

		anim.SetBool ("grounded", isGrounded);
		anim.SetFloat ("speed", Mathf.Abs(rb2d.velocity.x));
	}
}