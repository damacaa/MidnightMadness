using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPowerUp : PowerUp
{
    protected override void Effect()
    {
        AudioManager.instance.PlayOnce("recogerJeringuilla");
        if (!PlayerController.instance.injured)
            return;

        
        PlayerController.instance.Heal();
        Destroy(gameObject);
    }
}
