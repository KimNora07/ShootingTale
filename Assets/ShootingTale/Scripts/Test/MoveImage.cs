//System
using System.Collections;
using System.Collections.Generic;

//UnityEngine
using UnityEngine;

public class MoveImage : MonoBehaviour
{
    private RectTransform myRect;

    private void Awake()
    {
        myRect = GetComponent<RectTransform>();
    }
}
