using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePickup : ColorPickup {

    public override void MyFunction()
    {
        ColorManager.instance.blueUnlocked = true;
        ColorManager.instance.BlueMode();
    }
}
