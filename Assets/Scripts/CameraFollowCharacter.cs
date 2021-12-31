using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCharacter : MonoBehaviour
{
    public static CameraFollowCharacter instance;

    bool normalFollow = true;
    bool shake = false;
    public float shakeScale = .05f;
    float shakeTimer = 0;
    Coroutine currentCoroutine = null;
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

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.instance)
            return;

        if (shakeTimer > 0)
        {
            shake = true;
            shakeTimer -= Time.deltaTime;
        }
        else
            shake = false;

        if (normalFollow)
        {
            Vector3 pos;
            if (PlayerController.instance.vehicle)
            {
                pos = PlayerController.instance.vehicle.gameObject.transform.position;
            }
            else
            {
                pos = PlayerController.instance.transform.position;
            }
            pos.z = transform.position.z;

            if (shake)
            {
                float x = UnityEngine.Random.Range(-1f, 1f) * shakeScale;
                float y = UnityEngine.Random.Range(-1f, 1f) * shakeScale;
                pos += new Vector3(x, y, 0);
            }

            transform.position = pos;
        }
    }

    public void LookAt(Transform target)
    {
        normalFollow = false;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(GoTo(target.position));
    }

    public void BackToNormal()
    {
        
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(GoBackToPlayer());
    }

    IEnumerator GoTo(Vector3 targetPos, float time = .25f)
    {
        Vector3 originalPos = transform.position;
        float delta = 0;
        Vector3 pos;
        while (delta < time)
        {
            pos = Vector3.Lerp(originalPos, targetPos, delta / time);
            pos.z = transform.position.z;
            transform.position = pos;
            delta += Time.deltaTime;
            yield return null;
        }
        currentCoroutine = null;
        yield return null;
    }

    IEnumerator GoBackToPlayer(float time = .5f)
    {
        Vector3 originalPos = transform.position;
        float delta = 0;
        Vector3 pos;
        while (delta < time)
        {
            pos = Vector3.Lerp(originalPos, PlayerController.instance.transform.position, delta / time);
            pos.z = transform.position.z;
            transform.position = pos;
            delta += Time.deltaTime;
            yield return null;
        }
        currentCoroutine = null;
        normalFollow = true;
        yield return null;
    }

    public void ShakeCamera(float time)
    {
        shakeTimer = time;
    }
}
