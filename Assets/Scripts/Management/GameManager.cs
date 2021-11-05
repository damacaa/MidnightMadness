using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool gameEnd = false;

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

    public void EndGame()
    {
        gameEnd = true;
        UIManager.instance.ShowEnd();
        InputManager.instance.locked = true;
    }

    public void RestartGame()
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

        PlayerController.instance.attackController.Reload();
        PlayerController.instance.attackController.DropWeapon();
        PlayerController.instance.transform.position = Vector2.zero;
        PlayerController.instance.injured = false;
        EnemyFactoryManager.instance.Restart();
        ScoreManager.instance.Restart();
        InputManager.instance.locked = false;
    }

    public void GoToMenu()
    {

    }
}
