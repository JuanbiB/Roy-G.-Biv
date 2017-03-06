using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPickup : ColorPickup {

    public override void MyFunction()
    {
        Player.instance.redUnlocked = true;
        Player.instance.RedMode();
    }

}
