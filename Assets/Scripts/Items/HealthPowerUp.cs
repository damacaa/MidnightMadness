using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : PowerUp
{
    protected override bool Effect()
    {
        if (!PlayerController.instance.injured)
            return false;

        AudioManager.instance.PlayOnce("recogerCocacola");
        PlayerController.instance.Heal();
        return true;
    }
}
