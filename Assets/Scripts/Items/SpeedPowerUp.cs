using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUp
{
    protected override bool Effect()
    {
        AudioManager.instance.PlayOnce("recogerPastillas");


        GameManager.instance.IncreaseGameSpeed();
        PlayerController.instance.movementController.speed += 2f;
        Destroy(gameObject);
        return true;
    }
}
