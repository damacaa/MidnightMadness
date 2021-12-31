using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackController))]
[RequireComponent(typeof(MovementController))]
public class CharacterController : MonoBehaviour
{
    public bool isAwake = true;
    public float health = 10;

    protected Rigidbody2D rb;
    public MovementController movementController;
    public AttackController attackController;
    public VehicleController vehicle;

    protected void Awake()
    {
        movementController = GetComponent<MovementController>();
        attackController = GetComponent<AttackController>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Stun(float time, Vector2 dir = new Vector2())
    {
        if (!isAwake)
            return;

        attackController.DropWeapon();

        transform.up = -dir;

        rb.AddForce(dir * 100f, ForceMode2D.Impulse);

        isAwake = false;
        StartCoroutine(RecoverAfter(time));
        UpdateSprite();
    }

    IEnumerator RecoverAfter(float time)
    {
        yield return new WaitForSeconds(time);
        isAwake = true;
        UpdateSprite();
        yield return null;
    }

    public virtual void Hurt()
    {
        //Debug.Log("Ouch");
        GameManager.instance.SplashBlood(transform.position);
        Die();
        UpdateSprite();
    }

    protected virtual void Die()
    {
        //Debug.Log("Death");
        attackController.DropWeapon();
        UpdateSprite();
    }

    public void Move(float x, float y)
    {
        if (vehicle)
            vehicle.Move(x, y);
        else
            movementController.Move(x, y);
    }

    public void LookAtPlayer()
    {
        transform.up = PlayerController.instance.transform.position - transform.position;
    }

    public virtual void UpdateSprite() { }
    public virtual void AttackSprite() { }
}
