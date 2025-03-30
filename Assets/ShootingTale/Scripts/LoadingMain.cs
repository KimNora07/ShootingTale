//System
using System.Collections;
using System.Collections.Generic;

//UnityEngine
using UnityEngine;

public class LoadingMain : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadingManager.CoLoadSceneProgress());
    }
}
