//System

using UnityEngine;
//UnityEngine

public class FixResolution : MonoBehaviour
{
    private void Start()
    {
        SetResolution();
    }

    private void SetResolution()
    {
        var setWidth = 1920;
        var setHeight = 1080;

        Screen.SetResolution(setWidth, setHeight, true);
    }
}