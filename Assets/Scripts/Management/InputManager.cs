using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        PlayerController.instance.movementController.Move(x, y);

        if (Input.GetMouseButton(0)){
            PlayerController.instance.attackController.Attack();
        }

        if (Input.GetMouseButtonUp(0))
        {
            PlayerController.instance.attackController.Release();
        }

        if (Input.GetMouseButton(1))
        {
            PlayerController.instance.attackController.DropWeapon();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerController.instance.attackController.Reload();
        }
    }
}
