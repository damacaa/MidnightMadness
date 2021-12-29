using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : CharacterController
{
    CharacterController target;

    LayerMask playerMask;
    public float range = 10;
    public int score = 100;

    NavMeshAgent agent;

    private void Start()
    {
        playerMask = LayerMask.GetMask("Player");
        target = PlayerController.instance;
        range = attackController.Range;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.destination = transform.position;
    }

    protected override void Die()
    {
        ScoreManager.instance.AddScore(score);
        attackController.DropWeapon();
        EnemyFactoryManager.instance.DecreaseEnemyCounter();
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!target || GameManager.pause || !isAwake)
        {
            return;
        }

        agent.destination = target.transform.position;

        Vector2 dir = target.transform.position - transform.position;
        dir.Normalize();
        //movementController.Move(dir.x, dir.y);
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
