using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryManager : MonoBehaviour
{
    public static EnemyFactoryManager instance;
    public GameObject enemyPrefab;
    public float spawnWait;
    float nextSpawnTime;
    private bool working = true;

    public Transform[] spawnPoints;

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
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        nextSpawnTime = Time.time + spawnWait;
        int spawnPoint = Random.Range(0, spawnPoints.Length);

        GameObject.Instantiate(enemyPrefab, spawnPoints[spawnPoint].position, Quaternion.identity);
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
