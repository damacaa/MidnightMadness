using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public enum ShootingMode
    {
        Automatic,
        SemiAutomatic
    }
    public ShootingMode shootingMode = ShootingMode.Automatic;
    public int rateOfFire = 100;
    public int bulletVelocity = 10;
    public Transform exitPoint;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void Shoot()
    {
        Vector3 dir = exitPoint.position - transform.position;
        dir.Normalize();
        BulletBehaviour bullet = GameObject.Instantiate(bulletPrefab, exitPoint.position, Quaternion.identity).GetComponent<BulletBehaviour>();
        bullet.Shoot(transform.up, bulletVelocity);
    }
}
