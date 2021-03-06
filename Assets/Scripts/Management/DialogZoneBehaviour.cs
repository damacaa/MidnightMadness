using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]

public class DialogZoneBehaviour : MonoBehaviour
{
    public UnityEvent startEvent;
    public Dialog[] dialogs;
    public bool isOptional = true;
    public bool destroyAfter = false;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void StartDialog()
    {
        startEvent.Invoke();
        for (int i = 0; i < dialogs.Length; i++)
        {
            DialogManager.instance.AddDialog(dialogs[i]);
        }
        DialogManager.instance.StartDialog();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DialogManager.instance.selectedZone = this;
            if (isOptional)
            {
                UIManager.instance.ShowInteract();
            }
            else
            {
                if (destroyAfter)
                    dialogs[dialogs.Length - 1].endEvent.AddListener(DisableZone);
                StartDialog();
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DialogManager.instance.selectedZone = null;
            UIManager.instance.HideInteract();
        }
    }

    void DisableZone()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
