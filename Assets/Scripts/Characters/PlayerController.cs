using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    public static PlayerController instance;
    public AttackController attackController;
    public MovementController movementController;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        attackController = GetComponent<AttackController>();
        movementController = GetComponent<MovementController>();
    }

    private void Update()
    {
        // convert mouse position into world coordinates
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // get direction you want to point at
        Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
        // set vector of transform directly
        transform.up = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collison with: "+collision.gameObject.name);
        if (collision.collider.tag == "Bullet")
            Die();
    }

    public new void Die()
    {
        attackController.Reload();
        attackController.DropWeapon();
        EnemyFactoryManager.instance.Restart();
        ScoreManager.instance.Reset();
        transform.position = Vector2.zero;
    }
}
