using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactoryManager : MonoBehaviour
{
    public static ItemFactoryManager instance;
    public GameObject[] powerUpsPrefabs;
    public GameObject[] weaponPrefabs;

    public float powerUpSpawnWait;
    public float weaponSpawnWait;

    float nextPowerUpSpawnTime;
    float nextWeaponSpawnTime;
    private bool working = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        nextPowerUpSpawnTime = Time.time + (powerUpSpawnWait * 1.5f);
        nextWeaponSpawnTime = Time.time;
    }
    private void Update()
    {
        if (!working)
            return;

        if (Time.time > nextPowerUpSpawnTime)
        {
            SpawnPowerUp();
        }

        if (Time.time > nextWeaponSpawnTime)
        {
            SpawnWeapon();
        }
    }

    private void SpawnPowerUp()
    {
        nextPowerUpSpawnTime = Time.time + powerUpSpawnWait;

        int powerUpId = Random.Range(0, powerUpsPrefabs.Length);

        GameObject.Instantiate(powerUpsPrefabs[powerUpId], GetRandomPos(), Quaternion.identity);
    }

    private void SpawnWeapon()
    {
        nextWeaponSpawnTime = Time.time + weaponSpawnWait;

        int weaponpId = Random.Range(0, weaponPrefabs.Length);

        GameObject.Instantiate(weaponPrefabs[weaponpId], GetRandomPos(), Quaternion.identity);
    }

    float size = 5;
    Vector2 GetRandomPos()
    {
        float x = Random.Range(-size, size);
        float y = Random.Range(-size, size);

        return new Vector2(x, y);
    }

    public WeaponController GetRandomWeapon(Vector3 pos = new Vector3())
    {
        int i = Random.Range(0, weaponPrefabs.Length);
        GameObject g = GameObject.Instantiate(weaponPrefabs[i], pos, Quaternion.identity);
        return g.GetComponent<WeaponController>();
    }

    public GameObject GetRandomPowerUp(Vector3 pos)
    {
        int i = Random.Range(0, powerUpsPrefabs.Length);
        return GameObject.Instantiate(powerUpsPrefabs[i], pos, Quaternion.identity);
    }

    internal void Restart()
    {
        Continue();
    }

    public void Pause()
    {
        working = false;
    }

    public void Continue()
    {
        working = true;
        nextPowerUpSpawnTime = Time.time + powerUpSpawnWait;
    }
}
