using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoostPowerUp : PowerUp
{
    protected override void Effect()
    {
        AudioManager.instance.PlayOnce("recogerPorro");
        if (!PlayerController.instance.injured)
            return;

        
        PlayerController.instance.Heal();
        Destroy(gameObject);
    }
}
