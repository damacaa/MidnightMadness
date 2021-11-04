using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackController))]
[RequireComponent(typeof(MovementController))]
public class CharacterController : MonoBehaviour
{
    public bool isAwake = true;
    public float health = 10;

    void Start()
    {
        
    }

    public void Stun(float time)
    {
        isAwake = false;
        StartCoroutine(RecoverAfter(time));
    }

    IEnumerator RecoverAfter(float time)
    {
        yield return new WaitForSeconds(time);
        isAwake = true;
        yield return null;
    }

    protected virtual void Die() {
        Debug.Log("Ouch");
    }
}
