using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPickup : ColorPickup {

    public override void MyFunction()
    {
        CM.yellowUnlocked = true;
        CM.YellowMode();
    }

}
