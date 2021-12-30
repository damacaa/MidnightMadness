using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryManager : MonoBehaviour
{
    public static EnemyFactoryManager instance;

    public bool working = false;

    public GameObject enemyPrefab;
    public float spawnWait;
    float nextSpawnTime;

    public Transform[] spawnPoints;
    BusController[] buses;

    public int currentWave = 0;
    public int waves = 3;
    public int enemiesPerWave = 10;
    private float enemyCounter = 0;
    bool spawning = false;

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
        buses = GameObject.FindObjectsOfType<BusController>();

        StartGame();
    }

    public void StartGame()
    {
        if (!working)
            return;

        spawning = true;
        foreach (BusController b in buses)
        {
            b.RollIn();
        }

        StartCoroutine(SpawnEnemies(buses[0].time));
    }

    IEnumerator SpawnEnemies(float initialWait)
    {
        yield return new WaitForSeconds(initialWait);
        for (int i = 0; i < enemiesPerWave; i++)
        {
            int spawnPoint = Random.Range(0, spawnPoints.Length);
            GameObject.Instantiate(enemyPrefab, spawnPoints[spawnPoint].position, Quaternion.identity);
            enemyCounter++;
            yield return new WaitForSeconds(spawnWait);
        }

        foreach (BusController b in buses)
        {
            b.RollOut();
        }

        currentWave++;
        spawning = false;

        yield return null;
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

    public void DecreaseEnemyCounter()
    {
        enemyCounter--;
        if(enemyCounter == 0 && !spawning)
        {
            StartGame();
        }
    }
}
