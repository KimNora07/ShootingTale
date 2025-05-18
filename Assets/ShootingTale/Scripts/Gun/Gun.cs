using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletPoint = null;
    public GameObject bulletPrefab = null;

    public virtual void Shoot()
    {
        var go = ObjectPool.instance.GetObject();
        go.transform.position = this.bulletPoint.position;
    }
}
