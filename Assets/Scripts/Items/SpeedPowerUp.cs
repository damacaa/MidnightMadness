using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUp
{
    protected override void Effect()
    {
        AudioManager.instance.PlayOnce("recogerPastillas");
        if (!PlayerController.instance.injured)
            return;

        
        PlayerController.instance.Heal();
        Destroy(gameObject);
    }
}
