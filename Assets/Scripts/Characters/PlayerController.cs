using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    public static PlayerController instance;
    public bool injured = false;

    private void Start()
    {

    }
    private new void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        attackController = GetComponent<AttackController>();
        movementController = GetComponent<MovementController>();
    }

    private void Update()
    {
        if (GameManager.gameEnd || GameManager.pause || !isAwake)
            return;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collison with: "+collision.gameObject.name);
        if (collision.collider.tag == "Bullet")
            Hurt();

        if (collision.collider.tag == "Vehicle")
        {
            VehicleController v = collision.collider.GetComponent<VehicleController>();
            VehicleController.selectedVehicle = v;
        }


    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Vehicle")
        {
            VehicleController.selectedVehicle = null;
        }
    }

    private void Hurt()
    {
        if (GameManager.pause)
            return;

        if (injured)
        {
            Die();
        }
        else
        {
            injured = true;
        }
    }

    public void Heal()
    {
        injured = false;
    }

    public new void Die()
    {
        //GameManager.RestartGame();
        movementController.Move(0, 0);
        GameManager.EndGame();
    }

    public void GetInCar(VehicleController v)
    {
        Transform driverSeat;
        if (v.SetDriver(out driverSeat))
        {
            Debug.Log("Vehicle");
            transform.parent = driverSeat;
            vehicle = v;
            rb.isKinematic = true;
            GetComponent<Collider2D>().enabled = false;
            transform.position = driverSeat.position;
            transform.rotation = driverSeat.rotation;
        }
    }
    public void ExitCar()
    {
        transform.parent = null;
        vehicle = null;
        rb.isKinematic = false;
        GetComponent<Collider2D>().enabled = true;
        //transform.position = driverSeat.position;
    }
}
