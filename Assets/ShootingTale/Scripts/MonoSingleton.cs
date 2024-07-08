//System
using System.Collections;
using System.Collections.Generic;

//UnityEngine
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object lockObject = new object();
    private static T instance = null;
    private static bool isQuitting = false;

    public static T Instance
    {
        get
        {
            lock (lockObject)
            {
                if (isQuitting)
                    return null;

                if (instance == null)
                {
                    instance = new GameObject($"{typeof(T).Name}").AddComponent<T>();
                    DontDestroyOnLoad(instance.transform);
                }

                return instance;
            }
        }
    }

    private void OnDisable()
    {
        isQuitting = true;
        instance = null;
    }
}
