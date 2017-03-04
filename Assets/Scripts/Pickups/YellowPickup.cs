using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPickup : ColorPickup {

    public override void MyFunction()
    {
        GameManager.instance.yellowUnlocked = true;
        Player.YellowMode();
    }

}
