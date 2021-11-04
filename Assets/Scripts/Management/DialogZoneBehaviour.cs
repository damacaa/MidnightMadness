using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogZoneBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DialogManager.instance.AddDialog("Hola.", transform);
            DialogManager.instance.AddDialog("Hola, ¿qué tal estás?", PlayerController.instance.transform);
            DialogManager.instance.AddDialog("Muy bien, gracias.", transform);

            DialogManager.instance.StartDialog();
        }
    }
}
