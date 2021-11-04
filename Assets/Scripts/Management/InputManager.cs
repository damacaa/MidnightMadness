using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public bool locked = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            DialogManager.instance.NextDialog();
        }

        if (locked)
        {
            return;
        }

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
