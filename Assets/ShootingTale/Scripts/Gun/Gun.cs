using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletPoint = null;
    public GameObject bulletPrefab = null;

    public float bulletSpeed = default;

    public virtual void Shoot()
    {
        //GameObject go = Instantiate(this.bulletPrefab);

        GameObject go = ObjectPool.instance.GetObject();
        go.transform.position = this.bulletPoint.position;
    }


}
