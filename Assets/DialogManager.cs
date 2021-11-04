using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public Image dialogBox;
    public Text dialogText;

    bool creatingDialog = false;
    bool dialogReady = false;

    public int speed = 10;

    Queue<Dialog> dialogQueue = new Queue<Dialog>();

    Dialog currentDialog = null;
    Coroutine dialogCoroutine;
    Coroutine cameraCoroutine;

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

    void Start()
    {
        dialogBox.enabled = false;
        dialogQueue.Enqueue(new Dialog("Hola.", transform));
        dialogQueue.Enqueue(new Dialog("Hola, ¿qué tal estás?", PlayerController.instance.transform));
        dialogQueue.Enqueue(new Dialog("Muy bien, gracias.", transform));
        ShowDialog(dialogQueue.Dequeue());
    }

    void ShowDialog(Dialog dialog)
    {
        if (creatingDialog)
            return;

        currentDialog = dialog;

        dialogBox.enabled = true;
        dialogText.enabled = true;

        CameraFollowCharacter.instance.LookAt(dialog.target);
        InputManager.instance.locked = true;
        EnemyFactoryManager.instance.Restart();
        EnemyFactoryManager.instance.Pause();

        dialogCoroutine =  StartCoroutine(ShowDialogCoroutine(dialog.text));
    }

    IEnumerator ShowDialogCoroutine(string text)
    {
        creatingDialog = true;

        dialogText.text = "";
        float wait = 1f / speed;
        for (int i = 0; i < text.Length; i++)
        {
            dialogText.text += text[i];
            yield return new WaitForSeconds(wait);
        }

        creatingDialog = false;
        dialogReady = true;
        yield return null;
    }

    public void NextDialog()
    {
        if (creatingDialog)
        {
            StopCoroutine(dialogCoroutine);
            dialogText.text = currentDialog.text;

            creatingDialog = false;
            dialogReady = true;
        }
        else
        {
            if (dialogQueue.Count > 0)
            {
                ShowDialog(dialogQueue.Dequeue());
            }
            else
            {
                CloseDialog();
            }
        }
    }

    public void CloseDialog()
    {
        if (dialogReady)
        {
            dialogReady = false;
            dialogBox.enabled = false;
            dialogText.enabled = false;

            CameraFollowCharacter.instance.BackToNormal();
            InputManager.instance.locked = false;
            EnemyFactoryManager.instance.Continue();
        }
    }
}

public class Dialog
{
    public string text = "";
    public Transform target = null;

    public Dialog(string text, Transform target)
    {
        this.text = text;
        this.target = target;
    }
}
