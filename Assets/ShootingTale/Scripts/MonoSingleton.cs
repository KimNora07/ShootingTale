//System

using UnityEngine;
//UnityEngine

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static readonly object lockObject = new();
    private static T instance;
    private static bool isQuitting;

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