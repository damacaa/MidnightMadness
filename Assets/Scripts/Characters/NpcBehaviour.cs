using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBehaviour : CharacterController
{
    public bool isFriend = false;

    public override void Hurt()
    {
        LookAtPlayer();
    }
}
