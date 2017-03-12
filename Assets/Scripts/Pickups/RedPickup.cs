using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPickup : ColorPickup {

    // Red pickup will provide the player with the red power
    public override void MyFunction()
    {
        ColorManager.instance.redUnlocked = true;
        ColorManager.instance.RedMode();
    }

}
