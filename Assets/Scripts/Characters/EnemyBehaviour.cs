using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBehaviour : CharacterController
{
    CharacterController target;

    LayerMask playerMask;
    public float range = 10;
    public int score = 100;

    private void Start()
    {
        playerMask = LayerMask.GetMask("Player");
        target = PlayerController.instance;
        range = attackController.Range;
    }

    protected override void Die()
    {
        ScoreManager.instance.AddScore(score);
        attackController.DropWeapon();
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!target || GameManager.pause || !isAwake)
        {
            return;
        }

        Vector2 dir = target.transform.position - transform.position;
        dir.Normalize();
        movementController.Move(dir.x, dir.y);
        transform.up = dir;

        RaycastHit2D hit = Physics2D.Raycast(attackController.handPos.position, dir, attackController.Range, playerMask);
        Debug.DrawRay(attackController.handPos.position, dir * attackController.Range);
        if (hit.collider != null)
        {
            attackController.Attack();
            attackController.Release();
        }
    }
}
