using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletPoint;
    public GameObject bulletPrefab;

    public virtual void Shoot()
    {
        var go = ObjectPool.instance.GetObject();
        go.transform.position = bulletPoint.position;
    }
}