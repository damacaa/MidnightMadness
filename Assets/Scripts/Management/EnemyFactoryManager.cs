using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryManager : MonoBehaviour
{
    public static EnemyFactoryManager instance;
    public GameObject enemyPrefab;
    public float spawnWait;
    float nextSpawnTime;
    private bool working = false;

    Transform[] spawnPoints;

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
        spawnPoints = new Transform[transform.childCount];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
            working = true;
        }
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
        Vector3 screenPos = Camera.main.WorldToViewportPoint(spawnPoints[spawnPoint].position);
        int count = 0;
        while (screenPos.x > 0 && screenPos.x < 1 && count < spawnPoints.Length)
        {
            spawnPoint = Random.Range(0, spawnPoints.Length);
            count++;
        }

        if (count >= spawnPoints.Length)
            return;

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
