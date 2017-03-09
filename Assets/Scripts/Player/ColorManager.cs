using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
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
        ResetAttributes();
        currentMode = Mode.Red;
        sr.color = Color.red;
        controller.jumpVelocity = 25;
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

    // Change to yellow mode, where the player can run faster.
    public void YellowMode()
    {
        if (!yellowUnlocked)
        {
            return;
        }
        ResetAttributes();
        currentMode = Mode.Yellow;
        sr.color = Color.yellow;
        controller.speed = 15;
        if (blueUnlocked)
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
        if (!blueUnlocked)
        {
            return;
        }
        ResetAttributes();
        currentMode = Mode.Blue;
        sr.color = Color.blue;
        controller.gravity = 15;
        controller.jumpVelocity = 10;
        controller.glideMode = true;
        GameManager.instance.stateUI.sprite = GameManager.instance.stateOptions[9];
    }

	public void BlackMode()
	{
		ResetAttributes();
		currentMode = Mode.Black;
		sr.color = Color.black;
		controller.gravity = 80;
		controller.jumpVelocity = 2;
		StartCoroutine (WaitSeconds (5));

	}



	IEnumerator WaitSeconds(float seconds)
	{
		{Debug.Log ("wait seconds");

			yield return new  WaitForSeconds(seconds);}
		Debug.Log ("white mode");
		ResetAttributes ();
		WhiteMode();
	}

    

}
