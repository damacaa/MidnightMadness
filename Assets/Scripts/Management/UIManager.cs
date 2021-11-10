using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Text scoreText;
    public Text ammoText;
    public Text interactText;

    public Image dialogBox;
    public Text dialogText;
    public GameObject pauseMenu;
    public GameObject pauseButton;

    public GameObject endMenu;
    // Start is called before the first frame update
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

        endMenu.SetActive(false);
    }

    internal void ShowEnd()
    {
        endMenu.SetActive(true);
    }

    internal void HideEnd()
    {
        endMenu.SetActive(false);
    }

    void Start()
    {
        HideInteract();
        HideDialog();
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = Mathf.RoundToInt(ScoreManager.instance.score).ToString();
        ammoText.text = PlayerController.instance.attackController.GetAmmoString();
    }

    public void ShowInteract()
    {
        interactText.enabled = true;
    }

    public void HideInteract()
    {
        interactText.enabled = false;
    }

    public void ShowDialog()
    {
        dialogBox.enabled = true;
        dialogText.enabled = true;
    }

    public void HideDialog()
    {
        dialogBox.enabled = false;
        dialogText.enabled = false;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        GameManager.ResumeGame();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        GameManager.PauseGame();
    }

}
