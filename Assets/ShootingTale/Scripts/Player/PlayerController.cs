using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement2D movement2D;
    public GunEnums.EGunType eGunType;
    public Gun bullet;

    private WaitForSeconds shotCoolTime;
    private readonly float attackRate = 0.2f;

    private bool isShot = false;

    public void Init(Gun bullet)
    {
        this.bullet = bullet;
        this.bullet.transform.SetParent(this.transform);
        this.bullet.transform.localPosition = Vector3.zero;
    }

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        GameMain gameMain = GameObject.FindObjectOfType<GameMain>();
        gameMain.Init(this.eGunType);

        shotCoolTime = new WaitForSeconds(attackRate);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ShootBullet();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 플레이어 움직임
    /// </summary>
    private void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        movement2D.MoveTo(new Vector3(xInput, yInput, 0));  
    }

    private void ShootBullet()
    {
        if (!isShot)
        {
            this.bullet.Shoot();
            StartCoroutine(Co_ShotCoolTime());
        }
    }

    private IEnumerator Co_ShotCoolTime()
    {
        isShot = true;
        yield return shotCoolTime;
        isShot = false;
    }
}
