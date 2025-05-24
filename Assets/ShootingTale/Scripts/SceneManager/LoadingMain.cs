//System

using UnityEngine;
//UnityEngine

public class LoadingMain : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadingManager.CoLoadSceneProgress());
    }
}