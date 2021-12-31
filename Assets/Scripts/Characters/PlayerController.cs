using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    public static PlayerController instance;

    Animator animator;
    public bool injured = false;
    public bool infiniteHealth = false;

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-2500));
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
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.gameEnd || GameManager.pause || !isAwake)
            return;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<VehicleController>(out VehicleController v))
        {
            VehicleController.selectedVehicle = v;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<VehicleController>(out VehicleController v) && v == VehicleController.selectedVehicle)
        {
            VehicleController.selectedVehicle = null;
        }
    }

    public override void Hurt()
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
        if (v.GetIn(this, out driverSeat))
        {
            transform.parent = driverSeat;
            vehicle = v;
            rb.isKinematic = true;
            GetComponent<Collider2D>().enabled = false;
            transform.position = driverSeat.position;
            transform.rotation = driverSeat.rotation;
        }
        AudioManager.instance.PlayOnce("cocheArrancando");
    }

    public void ExitCar()
    {
        vehicle.GetOut(this);
        vehicle = null;
        transform.parent = null;
        rb.isKinematic = false;
        GetComponent<Collider2D>().enabled = true;
    }


    public override void UpdateSprite()
    {
        animator.SetBool("HasWeapon", attackController.weapon || attackController.attackingMelee);

        if (isAwake)
            animator.SetTrigger("Recover");
        else
            animator.SetTrigger("GetHurt");
    }
}
