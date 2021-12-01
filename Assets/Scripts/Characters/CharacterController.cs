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

        rb.AddForce(dir * 10f, ForceMode2D.Impulse);

        isAwake = false;
        StartCoroutine(RecoverAfter(time));
    }

    IEnumerator RecoverAfter(float time)
    {
        yield return new WaitForSeconds(time);
        isAwake = true;
        yield return null;
    }

    protected virtual void Die()
    {
        Debug.Log("Ouch");
    }


}
