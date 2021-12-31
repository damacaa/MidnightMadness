using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public bool locked = false;

    public bool controller = false;


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
        if (GameManager.gameEnd && Input.GetKey(KeyCode.R))
        {
            GameManager.RestartGame();
            return;
        }

        

        //Dialog interaction
        if (DialogManager.instance.selectedZone != null && Input.GetKeyDown(KeyCode.E) && !DialogManager.instance.dialogStarted)
        {
            DialogManager.instance.selectedZone.StartDialog();
            UIManager.instance.HideInteract();
            return;
        }
        else if (DialogManager.instance.dialogStarted && Input.anyKeyDown)
        {
            DialogManager.instance.NextDialog();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            UIManager.instance.PauseGame();

        if (GameManager.pause)
            return;

        if (VehicleController.selectedVehicle != null && Input.GetKeyDown(KeyCode.E))
        {
            PlayerController.instance.GetInCar(VehicleController.selectedVehicle);
            return;
        }



        if (locked || !PlayerController.instance.isAwake || !PlayerController.instance.movementController.canMove)
            return;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        PlayerController.instance.Move(x, y);

        if (PlayerController.instance.vehicle)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerController.instance.ExitCar();
            }
            return;
        }

        if (Input.GetButton("Fire1"))
        {
            PlayerController.instance.attackController.Attack();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            PlayerController.instance.attackController.Release();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            PlayerController.instance.attackController.ThrowWeapon();
        }

        if (Input.GetButtonDown("Reload"))
        {
            PlayerController.instance.attackController.Reload();
        }


        

        if (controller)
        {
            PlayerLookAtJoystick();
            return;
        }

        PlayerLookAtMouse();

    }

    void PlayerLookAtMouse()
    {
        // convert mouse position into world coordinates
        ////Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseScreenPosition = Input.mousePosition;
        //Debug.Log(mouseScreenPosition);
        // get direction you want to point at
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        ///Vector2 direction = (mouseScreenPosition - (Vector2)PlayerController.instance.transform.position).normalized;
        Vector2 direction = (mouseScreenPosition - center).normalized;
        // set vector of transform directly
        PlayerController.instance.transform.up = direction;
    }

    void PlayerLookAtJoystick()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        Debug.Log(x + ":" + y);

        PlayerController.instance.transform.up = new Vector2(x, y);
    }
}
