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
    public float range = 3;

    internal int currentAmmo;
    public int magSize = 10;
    public int ammo = 30;
    public float reloadTime;
    public bool reloading = false;

    internal bool flying = false;

    public Transform exitPoint;
    public GameObject flash;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        flash.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = magSize;
    }

    private void FixedUpdate()
    {
        if (flash.activeSelf)
            flash.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (flying && rigidBody.velocity.magnitude > 0 && rigidBody.velocity.magnitude < .5f)
        {
            flying = false;
        }
        else if (!flying && rigidBody.velocity.magnitude == 0 && rigidBody.angularVelocity == 0)
        {
            //rigidBody.isKinematic = true;
        }
    }

    float nextShootTime = 0;


    internal void Shoot()
    {
        if (reloading || currentAmmo <= 0)
            return;

        if (Time.time > nextShootTime)
        {
            if (shootingMode == ShootingMode.SemiAutomatic && triggerReleased)
            {
                triggerReleased = false;
                nextShootTime = Time.time + (1f / rateOfFire);
                ShootBullet();
                AudioManager.instance.PlayOnce("disparo");
                StartCoroutine(CameraShake(0.1f, 0.5f));
            }

            if (shootingMode == ShootingMode.Automatic)
            {
                nextShootTime = Time.time + (1f / rateOfFire);
                ShootBullet();
                AudioManager.instance.PlayOnce("disparo");
                StartCoroutine(CameraShake(0.1f,0.5f));
            }
        }
    }

    private void ShootBullet()
    {
        flash.SetActive(true);
        Vector3 dir = exitPoint.position - transform.position;
        dir.Normalize();
        BulletBehaviour bullet = BulletPool.instance.GetABullet(bulletType).GetComponent<BulletBehaviour>();
        bullet.transform.position = exitPoint.position;
        bullet.transform.rotation = exitPoint.rotation;
        bullet.Shoot(transform.up, bulletVelocity);
        currentAmmo--;
        if (currentAmmo <= 0)
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
        if (!reloading && currentAmmo < magSize && ammo > 0)
        {
            int ammoRequired = magSize - currentAmmo;//Check if player has enough ammo

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
        if (currentAmmo == 0)
            time *= 1.2f;

        yield return new WaitForSeconds(time);
        currentAmmo += newAmmo;
        ammo -= newAmmo;
        reloading = false;
        yield return null;
    }

    IEnumerator CameraShake(float time, float scale)
    {
        Camera camera = GameObject.FindObjectOfType<Camera>();

        float elapsedTime = 0.0f;
        Vector3 initialPosition = camera.transform.position;

        while (elapsedTime < time)
        {
           
            float x = UnityEngine.Random.Range(-0.1f, 0.1f)*scale;
            float y = UnityEngine.Random.Range(-0.1f, 0.1f)*scale;
            camera.transform.position += new Vector3(x, y, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        

        camera.transform.position = initialPosition;
        
    }
}
