using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : PowerUp
{
    protected override void Effect()
    {
        if (!PlayerController.instance.injured)
            return;

        PlayerController.instance.Heal();
        Destroy(gameObject);
    }

    
}
