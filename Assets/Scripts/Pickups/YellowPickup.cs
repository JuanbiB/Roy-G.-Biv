using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPickup : ColorPickup {

    public override void MyFunction()
    {
        Player.instance.yellowUnlocked = true;
        Player.instance.controller.YellowMode();
    }

}
