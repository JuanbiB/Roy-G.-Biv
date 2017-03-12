using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformerController))]
public class PlatformerInputModule : MonoBehaviour
{
    // MODIFIED PlatformerInputModule--takes input and directs it to the player's
    // various functions. Can manipulate the ColorManager and Controller.

    PlatformerController controller;
    ColorManager CM;

    void Start()
    {
        controller = GetComponent<PlatformerController>();
        CM = GetComponent<ColorManager>();
    }

    // If the player has control, checks for moves, jumps, or changes in color
    void Update()
    {
        // Don't take input if in Display Mode (Main Menu)
		if (Player.instance.inDisplay) {
			return;
		}

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CM.RedMode();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CM.YellowMode();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CM.BlueMode();
        }
        // ESC toggles the pause menu
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameManager.instance.paused)
            {
                GameManager.instance.Unpause();
            } else
            {
                GameManager.instance.Pause();
            }
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.magnitude > 1) {
            input.Normalize();
        }
        controller.input = input;
		controller.inputJump = Input.GetButtonDown("Jump");
    }
		
}
