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
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        nextSpawnTime = Time.time + spawnWait;
    }
    private void Update()
    {
        if (working && Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnWait;
            float x = Random.Range(-5f, 5f);
            float y = Random.Range(-3f, 3f);
            GameObject.Instantiate(enemyPrefab, new Vector2(x, y), Quaternion.identity);
        }
    }

    internal void Restart()
    {
        EnemyBehaviour[] enemies = FindObjectsOfType<EnemyBehaviour>();
        foreach (EnemyBehaviour enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        BulletBehaviour[] bullets = FindObjectsOfType<BulletBehaviour>();
        foreach (BulletBehaviour bullet in bullets)
        {
            bullet.gameObject.SetActive(false);
        }

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
