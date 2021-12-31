using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : CharacterController
{
    public static PlayerController instance;

    public Animator animator;
    public bool injured = false;
    public bool dead = false;
    public bool infiniteHealth = false;
    public GameObject bloodTrail;
    public GameObject throwUp;
    public Transform endGame;

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
        bloodTrail.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.instance.gameEnd || GameManager.instance.pause || !isAwake)
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
        if (GameManager.instance.pause || infiniteHealth)
            return;

        AudioManager.instance.PlayOnce("quejido1");
        GameManager.instance.SplashBlood(transform.position);

        if (injured)
        {
            Die();
        }
        else
        {
            injured = true;
            bloodTrail.SetActive(true);
        }
    }

    public void Heal()
    {
        injured = false;
        bloodTrail.SetActive(false);
    }

    public new void Die()
    {
        //GameManager.RestartGame();
        animator.SetTrigger("GetHurt");
        //animator.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        movementController.canMove = false;
        dead = true;
        //movementController.Move(0, 0);
        GameManager.instance.EndGame();
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

    public void ThrowUp()
    {
        animator.SetTrigger("ThrowUp");
        GameObject.Instantiate(throwUp).transform.position = transform.position - new Vector3(0, .3f, 0);
    }
    public void Recover()
    {
        animator.SetTrigger("Recover");
    }

    public void ComeOut()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -2500));
    }

    public void GoInside()
    {
        movementController.canMove = false;

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.destination = endGame.position;


    }
}
