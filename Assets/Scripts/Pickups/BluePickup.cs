using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePickup : ColorPickup {

    // Blue pickup will provide the player with the blue power
    public override void MyFunction()
    {
        ColorManager.instance.blueUnlocked = true;
        ColorManager.instance.BlueMode();
    }
}
