using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColorSelector : MonoBehaviour
{
    public Color paintColor = Color.white;
    public SpriteRenderer[] colorChangingParts;
    // Start is called before the first frame update
    private void Awake()
    {
        foreach (SpriteRenderer s in colorChangingParts)
        {
            s.color = paintColor;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        foreach (SpriteRenderer s in colorChangingParts)
        {
            s.color = paintColor;
        }
    }
#endif
}
