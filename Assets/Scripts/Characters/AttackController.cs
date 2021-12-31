using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform handPos;
    public bool attackingMelee = false;
    public WeaponController weapon = null;

    public float Range
    {
        get
        {
            if (weapon != null)
                return weapon.range;

            return .1f;
        }
    }

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
        else if (!attackingMelee)
        {
            MeleeAttack();
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
            return weapon.currentAmmo.ToString() + "/" + weapon.ammo.ToString();
        return "-/-";
    }

    public void PickupWeapon(WeaponController weap)
    {
        if (!handPos)
            return;

        weapon = weap;
        weapon.GetComponent<Collider2D>().enabled = false;
        weapon.rigidBody.velocity = Vector2.zero;
        weapon.rigidBody.angularVelocity = 0;
        weapon.rigidBody.isKinematic = true;
        weapon.transform.position = handPos.position;
        weapon.transform.rotation = handPos.rotation;
        weapon.transform.parent = handPos;

        if (weapon.currentAmmo == 0)
            weapon.Reload();
        AudioManager.instance.PlayOnce("recogerArma");
        GetComponentInParent<CharacterController>().UpdateSprite();
    }

    public void ThrowWeapon()
    {
        if (weapon == null)
            return;
        weapon.transform.parent = null;
        weapon.rigidBody.isKinematic = false;
        weapon.GetComponent<Collider2D>().enabled = true;
        //weapon.rigidBody.velocity = Vector2.zero;
        weapon.rigidBody.AddForce(transform.up * 300);
        weapon.rigidBody.AddTorque(-1f);
        weapon.StopAllCoroutines();
        weapon.reloading = false;
        weapon.flying = true;
        weapon = null;
        GetComponentInParent<CharacterController>().UpdateSprite();
    }

    public void DropWeapon()
    {
        if (weapon == null)
            return;
        weapon.transform.parent = null;
        weapon.rigidBody.isKinematic = false;
        weapon.GetComponent<Collider2D>().enabled = true;
        //weapon.rigidBody.velocity = Vector2.zero;
        //weapon.rigidBody.AddForce(transform.up * 300);
        //weapon.rigidBody.AddTorque(-3f);
        weapon.StopAllCoroutines();
        weapon.reloading = false;
        //weapon.flying = true;
        weapon = null;
        GetComponentInParent<CharacterController>().UpdateSprite();
    }

    public void MeleeAttack()
    {
        attackingMelee = true;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(handPos.position, Range, transform.up);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject == gameObject)
                continue;

            CharacterController cc;
            if (hits[i].collider.gameObject.TryGetComponent<CharacterController>(out cc))
            {
                Vector2 dir = hits[i].collider.transform.position - transform.position;
                dir.Normalize();
                cc.Stun(1f, dir);
            }
        }

        StartCoroutine(StopMeleeAfter(0.1f));
        GetComponentInParent<CharacterController>().UpdateSprite();
    }

    IEnumerator StopMeleeAfter(float time)
    {
        yield return new WaitForSeconds(time);
        StopMelee();
        yield return null;
    }

    public void StopMelee()
    {
        attackingMelee = false;
        GetComponentInParent<CharacterController>().UpdateSprite();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Weapon")
        {
            if (collision.collider.GetComponent<WeaponController>().flying)
            {
                GetComponent<CharacterController>().Stun(1f, collision.collider.GetComponent<Rigidbody2D>().velocity.normalized);
            }
            else
            {
                if (weapon == null && GetComponent<CharacterController>().isAwake)
                    PickupWeapon(collision.collider.GetComponent<WeaponController>());
            }
        }
    }
}
