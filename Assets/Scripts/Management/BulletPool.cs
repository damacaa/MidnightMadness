using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;

    public int nObjects = 100;
    public GameObject[] bulletPrefabs;
    List<GameObject> normalBullets;

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

    void Start()
    {
        normalBullets = new List<GameObject>();
        for (int i = 0; i < bulletPrefabs.Length; i++)
        {
            for (int j = 0; j < nObjects; j++)
            {
                normalBullets.Add(GameObject.Instantiate(bulletPrefabs[i]));
                normalBullets[j].SetActive(false);
            }
        }
    }

    public GameObject GetABullet(BulletType bullet)
    {
        List<GameObject> list = null;
        switch (bullet)
        {
            case BulletType.Normal:
                list = normalBullets;
                break;
            case BulletType.HighDamage:
                break;
            case BulletType.Sniper:
                break;
            case BulletType.Shotgun:
                break;
            default:
                break;
        }

        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].activeSelf)
            {
                list[i].SetActive(true);
                return list[i];
            }
        }

        GameObject newBullet = GameObject.Instantiate(bulletPrefabs[(int)bullet]);
        list.Add(newBullet);
        return newBullet;
    }
}

public enum BulletType
{
    Normal = 0,
    HighDamage = 1,
    Sniper = 2,
    Shotgun = 3
}
