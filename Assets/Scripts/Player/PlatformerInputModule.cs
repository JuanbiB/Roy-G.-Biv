﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformerController))]
public class PlatformerInputModule : MonoBehaviour
{
    PlatformerController controller;
	public bool inDisplay;

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

        if (Player.instance.redUnlocked && Input.GetKeyDown(KeyCode.Alpha1))
        {
            Player.instance.RedMode();
        }
        else if (Player.instance.yellowUnlocked && Input.GetKeyDown(KeyCode.Alpha2))
        {
            Player.instance.YellowMode();
        }
        else if (Player.instance.blueUnlocked && Input.GetKeyDown(KeyCode.Alpha3))
        {
            Player.instance.BlueMode();
        }
    }
		
}
