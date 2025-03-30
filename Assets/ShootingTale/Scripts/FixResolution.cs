//System
using System.Collections;
using System.Collections.Generic;

//UnityEngine
using UnityEngine;

public class FixResolution : MonoBehaviour
{
    private void Start()
    {
        SetResolution();
    }
    
    public void SetResolution()
    {
        int setWidth = 1920; 
        int setHeight = 1080; 
        
        Screen.SetResolution(setWidth, setHeight, true);
    }
}
