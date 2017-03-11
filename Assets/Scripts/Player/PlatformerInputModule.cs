using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformerController))]
public class PlatformerInputModule : MonoBehaviour
{
    PlatformerController controller;
    ColorManager CM;

    void Start()
    {
        controller = GetComponent<PlatformerController>();
        CM = GetComponent<ColorManager>();
    }

    void Update()
    {
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
