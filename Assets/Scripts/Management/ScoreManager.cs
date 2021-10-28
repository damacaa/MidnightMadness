using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public float score = 0;
    public float multiplier = 1;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime;
    }

    public void AddScore(int s)
    {
        score += s * multiplier;
    }

    private void ResetMultiplier()
    {
        multiplier = 1;
    }
}
