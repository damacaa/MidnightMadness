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
    }

    internal void ShowEnd()
    {
        throw new NotImplementedException();
    }

    void Start()
    {
        HideInteract();
        HideDialog();
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
}
