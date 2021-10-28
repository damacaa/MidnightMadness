using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform handPos;
    WeaponController weapon = null;
    
    // Start is called before the first frame update

    // Update is called once per frame
    public void Attack()
    {
        if(weapon!= null)
        {
            weapon.Shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            collision.transform.position = handPos.position;
            collision.transform.rotation = handPos.rotation;
            collision.transform.parent = handPos;
            weapon = collision.GetComponent<WeaponController>();
        }
    }
}
