using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    public static PlayerController instance;
    public AttackController attackController;
    public MovementController movementController;
    public bool injured = false;

    // Start is called before the first frame update
    private void Awake()
    {
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
        movementController.Move(0,0);
        GameManager.EndGame();
    }
}
