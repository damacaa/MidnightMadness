using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoostPowerUp : PowerUp
{
  protected override bool Effect()
    {
        if (PlayerController.instance.damageBoost)
            return false;
        
        AudioManager.instance.PlayOnce("recogerJeringuilla");
        PlayerController.instance.BoostDamage();
        return true;
    }
}
