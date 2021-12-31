using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameEnd = false;
    public float gameEndTime = 0;
    public bool pause = false;

    public GameObject bloodSplash;

    float gameSpeed = 1f;

    public DoormanBehaviour doorman;
    public GameObject doormanDialog;
    public GameObject doormanDialogEnd;
    public GameObject endDialog;

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

#if UNITY_EDITOR
        QualitySettings.vSyncCount = 1; // VSync must be disabled.
        Application.targetFrameRate = 60;
#endif

    }

    public void EndGame()
    {
        gameEnd = true;
        gameEndTime = Time.time;
        UIManager.instance.ShowEnd();
        InputManager.instance.locked = true;
    }

    public void RestartGame()
    {

        gameSpeed = 1f;
        /*gameEnd = false;

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
        PlayerController.instance.attackController.ThrowWeapon();
        PlayerController.instance.transform.position = Vector2.zero;
        PlayerController.instance.injured = false;
        EnemyFactoryManager.instance.Restart();
        ScoreManager.instance.Restart();
        InputManager.instance.locked = false;
        UIManager.instance.HideEnd();*/

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        pause = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pause = false;
        Time.timeScale = gameSpeed;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void SplashBlood(Vector3 pos)
    {
        GameObject.Instantiate(bloodSplash).transform.position = pos;
    }

    public void Victory()
    {
        doorman.MoveRight();
        doormanDialog.SetActive(false);
        doormanDialogEnd.SetActive(true);

        endDialog.SetActive(true);
    }

    public void IncreaseGameSpeed()
    {
        gameSpeed -= .05f;
        Time.timeScale = gameSpeed;
    }
}
