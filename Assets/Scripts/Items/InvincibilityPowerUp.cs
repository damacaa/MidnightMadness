using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPowerUp : PowerUp
{
    protected override bool Effect()
    {
        
        if (PlayerController.instance.infiniteHealth)
            return false;

        AudioManager.instance.PlayOnce("recogerPorro");
        PlayerController.instance.MakeInvincible();
        Destroy(gameObject);
        return true;
    }
}
