using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManagerMainMenu : MonoBehaviour
{
    public static UIManagerMainMenu instance;
    public Slider volumeBar;
    public GameObject menu;
    public GameObject options;
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

    // Start is called before the first frame update
    void Start()
    {
        volumeBar.value = AudioManager.instance.volume;
        volumeBar.onValueChanged.AddListener(delegate { UpdateBar(volumeBar.value); });
    }

    public void UpdateBar(float value)
    {
        AudioManager.instance.UpdateVolume(value);
    }

    public void StartGame()
    {
        GameManager.StartGame();
    }

    public void ExitGame()
    {
        GameManager.ExitGame();
    }

    public void ShowOptionsMenu()
    {
        for (int i = 0; i < menu.transform.childCount; i++)
        {
            var child = menu.transform.GetChild(i).gameObject;
            if (child != null)
                child.SetActive(false);
        }

        for (int i = 0; i < options.transform.childCount; i++)
        {
            var child = options.transform.GetChild(i).gameObject;
            if (child != null)
                child.SetActive(true);
        }
    }

    public void GoBack()
    {
        for (int i = 0; i < menu.transform.childCount; i++)
        {
            var child = menu.transform.GetChild(i).gameObject;
            if (child != null)
                child.SetActive(true);
        }

        for (int i = 0; i < options.transform.childCount; i++)
        {
            var child = options.transform.GetChild(i).gameObject;
            if (child != null)
                child.SetActive(false);
        }
    }

}
