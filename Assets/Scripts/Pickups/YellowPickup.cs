using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPickup : ColorPickup {

    // Yelolw pickup will provide the player with the yellow power
    public override void MyFunction()
    {
        ColorManager.instance.yellowUnlocked = true;
        ColorManager.instance.YellowMode();
    }

}
