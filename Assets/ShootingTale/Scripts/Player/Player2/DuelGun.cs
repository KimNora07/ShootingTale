using UnityEngine;

public class DuelGun : Gun
{
    public Transform bulletPoint2;

    public override void Shoot()
    {
        base.Shoot();

        var go = Instantiate(bulletPrefab);
        go.transform.position = bulletPoint2.position;
    }
}