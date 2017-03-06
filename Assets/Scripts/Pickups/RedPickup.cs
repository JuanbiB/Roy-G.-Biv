using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPickup : ColorPickup {

    public override void MyFunction()
    {
        CM.redUnlocked = true;
        CM.RedMode();
    }

}
