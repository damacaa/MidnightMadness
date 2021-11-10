using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform handPos;
    public WeaponController weapon = null;

    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).TryGetComponent<WeaponController>(out weapon);
        }

        if (weapon != null)
        {
            PickupWeapon(weapon);
        }
    }

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

    void PickupWeapon(WeaponController weap)
    {
        weapon = weap;
        weapon.GetComponent<Collider2D>().enabled = false;
        weapon.rigidBody.velocity = Vector2.zero;
        weapon.rigidBody.angularVelocity = 0;
        weapon.rigidBody.isKinematic = true;
        weapon.transform.position = handPos.position;
        weapon.transform.rotation = handPos.rotation;
        weapon.transform.parent = handPos;
    }

    public void DropWeapon()
    {
        if (weapon == null)
            return;
        weapon.transform.parent = null;
        weapon.rigidBody.isKinematic = false;
        weapon.GetComponent<Collider2D>().enabled = true;
        weapon.rigidBody.velocity = Vector2.zero;
        weapon.rigidBody.AddForce(transform.up * 100);
        weapon.rigidBody.AddTorque(-3f);
        weapon.StopAllCoroutines();
        weapon.reloading = false;
        weapon = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon" && weapon == null)
        {
            PickupWeapon(collision.GetComponent<WeaponController>());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Weapon" && weapon == null)
        {
            PickupWeapon(collision.collider.GetComponent<WeaponController>());
        }
    }
}
