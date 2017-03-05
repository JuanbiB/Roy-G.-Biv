using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float blacklifetime=5;
    // Instance of the player
    public static Player instance;

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
	//public bool blackUnlocked;

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

        // Start white, without having any powers unlocked
        instance.playerMode = Mode.White;
        redUnlocked = false;
        yellowUnlocked = false;
        blueUnlocked = false;
    }

    
    void Respawn()
    {

    }
	public void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("obstacle")) {


			Destroy (gameObject);
		}
	
		else if (other.CompareTag("raindrop") && instance.playerMode!=Mode.Black) {
			StartCoroutine (WaitSeconds (blacklifetime));
			instance.playerMode = Mode.White;

		}

	}
	IEnumerator WaitSeconds(float seconds){
		instance.playerMode = Mode.Black;
		yield return new  WaitForSeconds(seconds);
	


				

	}

	public void Die(){
		gameObject.GetComponent<PlatformerInputModule> ().inDisplay = true;
		controller.gravity = 0;
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -1, 0);
	}

    
}
