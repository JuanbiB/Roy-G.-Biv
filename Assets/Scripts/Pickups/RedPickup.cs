using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPickup : ColorPickup {

    public override void MyFunction()
    {
        ColorManager.instance.redUnlocked = true;
        ColorManager.instance.RedMode();
    }

}
