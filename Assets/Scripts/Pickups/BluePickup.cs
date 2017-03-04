using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePickup : ColorPickup {

    public override void MyFunction()
    {
        GameManager.instance.blueUnlocked = true;
        Player.BlueMode();
    }

}
