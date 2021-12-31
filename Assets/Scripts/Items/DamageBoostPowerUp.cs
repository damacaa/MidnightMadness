using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoostPowerUp : PowerUp
{
    protected override void Effect()
    {

        if (!PlayerController.instance.injured)
            return;

        AudioManager.instance.PlayOnce("recogerCocacola");
        PlayerController.instance.Heal();
        Destroy(gameObject);
    }
}
