using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        PlayerController.instance.movementController.Move(x, y);

        if (Input.GetMouseButtonDown(0)){
            PlayerController.instance.attackController.Attack();
        }
    }
}
