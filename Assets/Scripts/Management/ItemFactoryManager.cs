using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactoryManager : MonoBehaviour
{
    public static ItemFactoryManager instance;
    public GameObject[] powerUpsPrefabs;
    public float spawnWait;
    float nextSpawnTime;
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
        nextSpawnTime = Time.time + spawnWait;
    }
    private void Update()
    {
        if (working && Time.time > nextSpawnTime)
        {
            SpawnPowerUp();
        }
    }

    private void SpawnPowerUp()
    {
        nextSpawnTime = Time.time + spawnWait;

        int powerUpId = Random.Range(0, powerUpsPrefabs.Length);

        GameObject.Instantiate(powerUpsPrefabs[powerUpId], transform.position, Quaternion.identity);
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
        nextSpawnTime = Time.time + spawnWait;
    }
}
