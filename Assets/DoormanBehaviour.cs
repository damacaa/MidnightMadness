using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoormanBehaviour : MonoBehaviour
{
    public void GoToPosition()
    {
        transform.position = new Vector3(0, -2);
    }

    public void MoveRight()
    {
        transform.position = new Vector3(2, -2);
    }
}
