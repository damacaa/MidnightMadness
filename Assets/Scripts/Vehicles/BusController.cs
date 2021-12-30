using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusController : MonoBehaviour
{
    public Vector3 posInside;
    public Vector3 posOutside;
    Coroutine coroutine;
    public float time = 3;
    void Awake()
    {
        posInside = transform.position;
        posOutside = posInside - (30 * transform.up);

        transform.position = posOutside;
    }

    public void RollIn()
    {
        StopAllCoroutines();
        coroutine = StartCoroutine(RollInCoroutine());
    }

    public void RollOut() {
        StopAllCoroutines();
        coroutine = StartCoroutine(RollOutCoroutine());
    }

    IEnumerator RollInCoroutine()
    {
        float t = 0;
        while (t <= time)
        {
            transform.position = Vector3.Lerp(posOutside, posInside, t / time);
            t += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    IEnumerator RollOutCoroutine()
    {
        float t = 0;
        while (t <= time)
        {
            transform.position = Vector3.Lerp(posInside, posOutside, t / time);
            t += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
