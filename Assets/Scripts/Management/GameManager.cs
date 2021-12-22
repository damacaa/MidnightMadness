using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool gameEnd = false;
    public static bool pause = false;

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

    public static void EndGame()
    {
        gameEnd = true;
        UIManager.instance.ShowEnd();
        InputManager.instance.locked = true;
    }

    public static void RestartGame()
    {
        gameEnd = false;

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
        UIManager.instance.HideEnd();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void PauseGame()
    {
        pause = true;
        Time.timeScale = 0;
    }

    public static void ResumeGame()
    {
        pause = false;
        Time.timeScale = 1;
    }

    public static void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public static void ExitGame()
    {
        Application.Quit();
    }

}
