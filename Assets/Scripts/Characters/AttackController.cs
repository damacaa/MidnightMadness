using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform handPos;
    public WeaponController weapon = null;

    // Start is called before the first frame update

    // Update is called once per frame
    public void Attack()
    {
        if (weapon != null)
        {
            weapon.Shoot();
        }
    }

    public void Reload()
    {
        if (weapon != null)
            weapon.Reload();
    }

    public void Release()
    {
        if (weapon != null)
            weapon.ReleaseTrigger();
    }

    public string GetAmmoString()
    {
        if (weapon != null)
            return weapon.ammoInMag.ToString() + "/" + weapon.ammo.ToString();
        return "-/-";
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
