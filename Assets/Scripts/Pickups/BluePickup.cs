using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePickup : ColorPickup {

    public override void MyFunction()
    {
        CM.blueUnlocked = true;
        CM.BlueMode();
    }

}
