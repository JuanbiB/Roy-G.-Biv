using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePickup : ColorPickup {

    public override void MyFunction()
    {
        Player.instance.blueUnlocked = true;
        Player.instance.controller.BlueMode();
    }

}
