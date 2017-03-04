using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPickup : ColorPickup {

    public override void MyFunction()
    {
        GameManager.instance.redUnlocked = true;
        Player.RedMode();
    }

}
