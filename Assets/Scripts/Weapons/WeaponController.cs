using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public BulletType bulletType = BulletType.Normal;

    internal Rigidbody2D rigidBody;
    public enum ShootingMode
    {
        Automatic,
        SemiAutomatic
    }
    public ShootingMode shootingMode = ShootingMode.Automatic;
    bool triggerReleased = true;

    public int rateOfFire = 100;
    public int bulletVelocity = 10;

    internal int ammoInMag;
    public int magSize = 10;
    public int ammo = 30;
    public float reloadTime;
    public bool reloading = false;

    public Transform exitPoint;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ammoInMag = magSize;
    }

    // Update is called once per frame
    void Update()
    {

    }

    float nextShootTime = 0;
    internal void Shoot()
    {
        if (reloading || ammoInMag <= 0)
            return;

        if(Time.time > nextShootTime)
        {
            if (shootingMode == ShootingMode.SemiAutomatic && triggerReleased)
            {
                triggerReleased = false;
                nextShootTime = Time.time + (1f / rateOfFire);
                ShootBullet();
            }

            if (shootingMode == ShootingMode.Automatic)
            {
                nextShootTime = Time.time + (1f / rateOfFire);
                ShootBullet();
            }
        }
    }

    private void ShootBullet()
    {
        Vector3 dir = exitPoint.position - transform.position;
        dir.Normalize();
        BulletBehaviour bullet = BulletPool.instance.GetABullet(bulletType).GetComponent<BulletBehaviour>();
        bullet.transform.position = exitPoint.position;
        bullet.transform.rotation = exitPoint.rotation;
        bullet.Shoot(transform.up, bulletVelocity);
        ammoInMag--;
        if (ammoInMag <= 0)
        {
            Reload();
        }
    }

    internal void ReleaseTrigger()
    {
        triggerReleased = true;
    }

    public void Reload()
    {
        if (!reloading && ammoInMag < magSize && ammo > 0)
        {
            int ammoRequired = magSize - ammoInMag;//Check if player has enough ammo

            if (ammoRequired <= ammo)
            {
                StartCoroutine(WaitReload(reloadTime, ammoRequired));//Check if full reload
            }
            else
            {
                StartCoroutine(WaitReload(reloadTime, ammo));//Check if full reload
            }

            reloading = true;
        }
    }

    IEnumerator WaitReload(float time, int newAmmo)
    {
        yield return new WaitForSeconds(time);
        ammoInMag += newAmmo;
        ammo -= newAmmo;
        reloading = false;
        yield return null;
    }
}
