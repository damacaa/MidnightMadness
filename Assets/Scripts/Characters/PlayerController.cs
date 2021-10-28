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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
