using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField] private GameObject prefab;
    private readonly Queue<GameObject> prefabPoolQueue = new();

    private void Awake()
    {
        instance = this;
        CreateAndAddPoolObject(5);
    }

    private void CreateAndAddPoolObject(int count)
    {
        for (var i = 0; i < count; ++i) prefabPoolQueue.Enqueue(CreateObject());
    }

    public GameObject CreateObject()
    {
        var obj = Instantiate(prefab);
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);

        return obj;
    }

    public GameObject GetObject()
    {
        var obj = prefabPoolQueue.Count <= 0 ? CreateObject() : prefabPoolQueue.Dequeue();
        obj.gameObject.SetActive(true);
        obj.transform.SetParent(null);

        return obj;
    }

    public void ResetForPool(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        prefabPoolQueue.Enqueue(obj);
    }
}