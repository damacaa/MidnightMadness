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

    SpriteRenderer spriteRenderer;
    public Sprite normalSprite;
    public Sprite attackSprite;
    public Sprite hurtSprite;

    private void Start()
    {
        playerMask = LayerMask.GetMask("Player");
        target = PlayerController.instance;
        range = attackController.Range;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.destination = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = normalSprite;
    }

    protected override void Die()
    {
        ScoreManager.instance.AddScore(score);
        attackController.DropWeapon();
        EnemyFactoryManager.instance.DecreaseEnemyCounter();
        isAwake = false;
        spriteRenderer.sprite = hurtSprite;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(DestroyAfter(5f));
    }

    IEnumerator DestroyAfter(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
        GameManager.instance.SplashBlood(transform.position);
        yield return null;
    }

    private void Update()
    {
        if (!target || GameManager.pause || !isAwake || !PlayerController.instance.isAwake)
        {
            agent.enabled = false;
            return;
        }

        agent.enabled = true;
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

    public override void UpdateSprite()
    {
        if (!isAwake)
            spriteRenderer.sprite = hurtSprite;
        else if (attackController.weapon || attackController.attackingMelee)
            spriteRenderer.sprite = attackSprite;
        else
            spriteRenderer.sprite = normalSprite;
    }


}
