using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public DialogZoneBehaviour selectedZone = null;
 

    public bool dialogStarted = false;

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
       
    }

    public void AddDialog(string text, Transform target)
    {
        dialogQueue.Enqueue(new Dialog(text, target));
    }

    public void AddDialog(Dialog dialog)
    {
        dialogQueue.Enqueue(dialog);
    }

    public void StartDialog()
    {
        ShowDialog(dialogQueue.Dequeue());
        PlayerController.instance.movementController.Move(0,0);
    }

    void ShowDialog(Dialog dialog)
    {
        if (creatingDialog)
            return;

        currentDialog = dialog;

        dialogStarted = true;
        GameManager.pause = true;

        UIManager.instance.ShowDialog();

        CameraFollowCharacter.instance.LookAt(dialog.target);
        InputManager.instance.locked = true;
        EnemyFactoryManager.instance.Restart();
        EnemyFactoryManager.instance.Pause();

        dialogCoroutine =  StartCoroutine(ShowDialogCoroutine(dialog.text));
    }

    IEnumerator ShowDialogCoroutine(string text)
    {
        creatingDialog = true;

        UIManager.instance.dialogText.text = "";
        float wait = 1f / speed;
        for (int i = 0; i < text.Length; i++)
        {
            UIManager.instance.dialogText.text += text[i];
            yield return new WaitForSeconds(wait);
        }

        if (currentDialog.endEvent != null)
        {
            currentDialog.endEvent.Invoke();
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
            UIManager.instance.dialogText.text = currentDialog.text;

            if (currentDialog.endEvent != null)
            {
                currentDialog.endEvent.Invoke();
            }

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
            dialogStarted = false;
            UIManager.instance.HideDialog();

            CameraFollowCharacter.instance.BackToNormal();
            InputManager.instance.locked = false;
            EnemyFactoryManager.instance.Continue();
            GameManager.pause = false;


        }
    }
}

[System.Serializable]
public class Dialog
{
    public string text = "";
    public Transform target = null;
    public UnityEvent endEvent;

    public Dialog(string text, Transform target)
    {
        this.text = text;
        this.target = target;
    }
}
