using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactoryManager : MonoBehaviour
{
    public static ItemFactoryManager instance;
    public GameObject[] powerUpsPrefabs;
    public GameObject[] weaponPrefabs;

    public float spawnWait;

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
        nextPowerUpSpawnTime = Time.time + (spawnWait * 1.5f);
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
        else if (Time.time > nextWeaponSpawnTime)
        {
            SpawnWeapon();
        }
    }

    private void SpawnPowerUp()
    {
        nextPowerUpSpawnTime = Time.time + spawnWait;

        int powerUpId = Random.Range(0, powerUpsPrefabs.Length);

        GameObject.Instantiate(powerUpsPrefabs[powerUpId], GetRandomPos(), Quaternion.identity);
    }

    private void SpawnWeapon()
    {
        nextWeaponSpawnTime = Time.time + spawnWait;

        int weaponpId = Random.Range(0, weaponPrefabs.Length);

        GameObject.Instantiate(weaponPrefabs[weaponpId], GetRandomPos(), Quaternion.identity);
    }

    float size = 5;
    Vector2 GetRandomPos()
    {
        float x = Random.Range(-size,size);
        float y = Random.Range(-size,size);

        return new Vector2(x,y);
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
        nextPowerUpSpawnTime = Time.time + spawnWait;
    }
}
