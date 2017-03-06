﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Instance of the player
    public static Player instance;

    RaycastHit2D hitSideRay;
    public LayerMask playerMask;

    // Different modes for the player
    public enum Mode { White, Red, Yellow, Blue, Black }

    // Current mode of the player
    public Mode playerMode;
	 
    // Controller of the player
    public PlatformerController controller;

    // Booleans representing if the player has unlocked colors
    public bool redUnlocked;
    public bool yellowUnlocked;
    public bool blueUnlocked;

    // Duration of the Black Mode debuff
    public float blacklifetime = 5;

    // Keeps track of player's latest checkpoint
    Vector2 checkPoint;

	// Stores the original value of gravity used in respawn
	float originalGravity;

    void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set instance to this
            instance = this;
        }

        // If instance already exists and it's not this:
        else if (instance != this)
        {
            // Then destroy this, enforcing the Singleton pattern.
            Destroy(gameObject);
        }

        // Sets this to not be destroyed when loading a new scene.
        DontDestroyOnLoad(gameObject);
        
    }

    // Use this for initialization
    void Start ()
    {
        // Register the controller
        controller = gameObject.GetComponent<PlatformerController>();
		originalGravity = controller.gravity;

        // Start white, without having any powers unlocked
        instance.playerMode = Mode.White;
        redUnlocked = false;
        yellowUnlocked = false;
        blueUnlocked = false;

		// first checkpoint is always at beginning of the level
		checkPoint = transform.position;
    }

    public void SetCheckpoint()
    {
        checkPoint = transform.position;
    }
    
    public void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("obstacle")) {
			Destroy (gameObject);
		}
	
		else if (other.CompareTag("raindrop") && instance.playerMode!=Mode.Black) {
			StartCoroutine (WaitSeconds (blacklifetime));
			instance.playerMode = Mode.White;
		}

        else if(other.CompareTag("Checkpoint"))
        {
            SetCheckpoint();
            SpriteRenderer flagsr = other.gameObject.GetComponent<SpriteRenderer>();
            flagsr.color = Color.black;
        }
    }

    IEnumerator WaitSeconds(float seconds){
		instance.playerMode = Mode.Black;
		yield return new  WaitForSeconds(seconds);
	}

	IEnumerator BeginRespawn(float seconds){
		yield return new WaitForSeconds (seconds);

		// return control to the player
		gameObject.GetComponent<PlatformerInputModule> ().inDisplay = false;

		// reset position to latest checkpoint
		gameObject.transform.position = checkPoint;

		// reset velocity to zero
		gameObject.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;

		// reset dead boolean in the animator
		gameObject.GetComponent<Animator> ().SetBool ("dead", false);

		// reset gravity 
		controller.gravity = originalGravity;
	}

	public void Die(){
		// Prevent player from moving 
		gameObject.GetComponent<PlatformerInputModule> ().inDisplay = true;

		// storing the original gravity value
		originalGravity = controller.gravity;
		controller.gravity = 0;

		// Slow velocity as they enter the cloud
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -1, 0);
		// Begin death animation
		gameObject.GetComponent<Animator> ().SetBool ("dead", true);

		// Start coroutine that'll reset the player's location to last checkpoint
		StartCoroutine(BeginRespawn(2f));

	}

    // Reset defaults to avoid retaining characteristics from other modes.
    void ResetAttributes()
    {
        controller.jumpVelocity = 15;
        controller.speed = 5;
        controller.gravity = 40;
    }

    public void WhiteMode()
    {
        instance.playerMode = Mode.White;
        controller.sr.color = Color.white;
        if (instance.blueUnlocked)
        {
            GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[6];
        }
        else if (instance.yellowUnlocked)
        {
            GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[3];
        }
        else if (instance.redUnlocked)
        {
            GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[1];
        }
    }

    // Change to red mode, where the player can jump higher.
    public void RedMode()
    {
        ResetAttributes();
        instance.playerMode = Mode.Red;
        controller.sr.color = Color.red;
        controller.jumpVelocity = 25;
        if (instance.blueUnlocked)
        {
            GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[7];
        }
        else if (instance.yellowUnlocked)
        {
            GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[4];
        }
        else
        {
            GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[2];
        }
    }

    // Change to yellow mode, where the player can run faster.
    public void YellowMode()
    {
        ResetAttributes();
        instance.playerMode = Mode.Yellow;
        controller.sr.color = Color.yellow;
        controller.speed = 15;
        if (instance.blueUnlocked)
        {
            GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[8];
        }
        else
        {
            GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[5];
        }
    }

    //Change to blue mode, where the player glides.
    public void BlueMode()
    {
        ResetAttributes();
        instance.playerMode = Mode.Blue;
        controller.sr.color = Color.blue;
        controller.gravity = 15;
        controller.jumpVelocity = 10;
        GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[9];
    }

    public void BlackMode()
    {
        ResetAttributes();
        instance.playerMode = Mode.Black;
        controller.sr.color = Color.black;
        controller.gravity = 80;
        controller.jumpVelocity = 2;
        GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[0];
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
            }
            else if (hitSideNormal == hitSideRay.transform.right)
            {
                Debug.Log("right");
            }
            else if (hitSideNormal == -hitSideRay.transform.right)
            {
                Debug.Log("left");
            }
            else if (hitSideNormal == -hitSideRay.transform.up)
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
