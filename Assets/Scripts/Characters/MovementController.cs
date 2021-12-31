using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rb;
    CharacterController cc;
    public float speed = 10f;
    public bool canMove = true;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CharacterController>();
    }

    public void Move(float x, float y)
    {
        if (!canMove)
            return;
        rb.velocity = speed * new Vector2(x, y);
    }

    public void EnableMovement()
    {
        canMove = true;
        if(TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.animator.SetTrigger("Recover");
        }
    }
}
