using UnityEngine;

public class DuelGun : Gun
{
    public Transform bulletPoint2 = null;

    public override void Shoot()
    {
        base.Shoot();

        var go = Instantiate(this.bulletPrefab);
        go.transform.position = this.bulletPoint2.position;
    }
}
