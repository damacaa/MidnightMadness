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
        rb.velocity = speed * new Vector2(x, y);
    }

    //Needs to go somewhere else
    void Update()
    {
        
    }
}
