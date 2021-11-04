using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBehaviour : CharacterController
{
    CharacterController target;
    MovementController movementController;
    AttackController attackController;
    LayerMask playerMask;
    public float range = 10;
    public int score = 100;

    private void Awake()
    {
        movementController = GetComponent<MovementController>();
        attackController = GetComponent<AttackController>();
        playerMask = LayerMask.GetMask("Player");
    }

    private void Start()
    {
        target = PlayerController.instance;
    }

    public new void Die()
    {
        ScoreManager.instance.AddScore(score);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!target)
            return;

        Vector2 dir = target.transform.position - transform.position;
        dir.Normalize();
        movementController.Move(dir.x, dir.y);
        transform.up = dir;

        RaycastHit2D hit = Physics2D.Raycast(attackController.handPos.position, dir, range, playerMask);
        Debug.DrawRay(attackController.handPos.position, dir * range);
        if (hit.collider != null)
        {
            attackController.Attack();
            attackController.Release();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collison with: "+collision.gameObject.name);
        if (collision.collider.tag == "Bullet")
            Die();
    }
}
