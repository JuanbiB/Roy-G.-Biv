using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;

    PlatformerController controller;
    SpriteRenderer sr;

    // Different modes for the player
    public enum Mode { White, Red, Yellow, Blue, Black }

    // Current mode of the player
    Mode currentMode;

    // Booleans representing if the player has unlocked colors
    public bool redUnlocked;
    public bool yellowUnlocked;
    public bool blueUnlocked;

    // Duration of the Black Mode debuff
    float blacklifetime = 5;

    public AudioClip DeathSound;

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
    void Start()
    {
        controller = gameObject.GetComponent<PlatformerController>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        // Start white, without having any powers unlocked
        currentMode = Mode.White;
        redUnlocked = false;
        yellowUnlocked = false;
        blueUnlocked = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("raindrop"))
        {
            AudioSource.PlayClipAtPoint(DeathSound, transform.position);
            BlackMode();
        }
    }

    // Reset defaults to avoid retaining characteristics from other modes.
    void ResetAttributes()
    {
        controller.jumpVelocity = 15;
        controller.speed = 5;
        controller.gravity = 40;
        controller.glideMode = false;
    }

    public void WhiteMode()
    {
        ResetAttributes();
        currentMode = Mode.White;
        sr.color = Color.white;
        if (blueUnlocked)
        {
            GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[6];
        }
        else if (yellowUnlocked)
        {
            GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[3];
        }
        else if (redUnlocked)
        {
            GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[1];
        }
    }

    // Change to red mode, where the player can jump higher.
    public void RedMode()
    {
        if(!redUnlocked)
        {
            return;
        }
        if (currentMode == Mode.Red)
        {
            WhiteMode();
        } else { 
            ResetAttributes();
            currentMode = Mode.Red;
            sr.color = Color.red;
            controller.jumpVelocity = 25;
            controller.glideMode = false;
            if (blueUnlocked)
            {
                GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[7];
            }
            else if (yellowUnlocked)
            {
                GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[4];
            }
            else
            {
                GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[2];
            }
        }
    }

    // Change to yellow mode, where the player can run faster.
    public void YellowMode()
    {
        if (!yellowUnlocked)
        {
            return;
        }
        if(currentMode == Mode.Yellow)
        {
            ResetAttributes();
            currentMode = Mode.Yellow;
            sr.color = Color.yellow;
            controller.speed = 15;
            controller.glideMode = false;
            if (blueUnlocked)
            {
                GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[8];
            }
            else
            {
                GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[5];
            }
        }
    }

    //Change to blue mode, where the player glides.
    public void BlueMode()
    {
        if (!blueUnlocked)
        {
            return;
        }
        if(currentMode == Mode.Blue)
        {
            WhiteMode();
        } else
        {
            ResetAttributes();
            currentMode = Mode.Blue;
            sr.color = Color.blue;
            controller.gravity = 15;
            controller.jumpVelocity = 10;
            controller.glideMode = true;
            GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[9];
        }
    }

	public bool prevyellow, prevred, prevblue;

	public void BlackMode()
	{
		if (yellowUnlocked) {
			prevyellow = true;
			yellowUnlocked = false;
		}
		if (redUnlocked) {
			prevred = true;
			redUnlocked = false;
		}
		if (blueUnlocked) {
			prevblue = true;
			blueUnlocked = false;
		}
        
		currentMode = Mode.Black;
		sr.color = Color.black;
		controller.gravity = 80;
		controller.jumpVelocity = 2;
        controller.speed = 3;
		StartCoroutine (WaitSeconds (blacklifetime));
    }


	IEnumerator WaitSeconds(float seconds)
	{
	    yield return new  WaitForSeconds(seconds);

		if (prevyellow) {
			yellowUnlocked = true;
		}
		if (prevblue) {
			blueUnlocked = true;
		}
		if (prevred) {
			redUnlocked=true;
		}

        if(currentMode == Mode.Black)
        {
            WhiteMode();
        }
    }
}
