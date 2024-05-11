using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement2D movement2D;
    public GunEnums.EGunType eGunType;
    public Gun bullet;

    public SpriteRenderer spriteRenderer;
    public Sprite normalSprite;
    public Sprite canShotSprite;

    private WaitForSeconds shotCoolTime;
    private WaitForSeconds fightCoolTime;
    private readonly float attackRate = 0.2f;
    private readonly float fightRate = 10.0f;

    private bool isShot = false;
    private bool isFight = false;

    public void Init(Gun bullet)
    {
        this.bullet = bullet;
        this.bullet.transform.SetParent(this.transform);
        this.bullet.transform.localPosition = Vector3.zero;
        this.spriteRenderer.sprite = this.normalSprite;
    }

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        GameMain gameMain = GameObject.FindObjectOfType<GameMain>();
        gameMain.Init(this.eGunType);

        shotCoolTime = new WaitForSeconds(attackRate);
        fightCoolTime = new WaitForSeconds(fightRate);
    }

    private void Update()
    {
        ShootBullet();
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!isShot && isFight)
            {
                this.bullet.Shoot();
                StartCoroutine(Co_ShotCoolTime());
            }
        }
    }

    private IEnumerator Co_ShotCoolTime()
    {
        isShot = true;
        yield return shotCoolTime;
        isShot = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FightSign"))
        {
            if (Input.GetKeyDown(KeyCode.Z) && !isFight)
            {
                StartCoroutine(Co_FightCoolTime());
                Destroy(collision.gameObject);
            }
        }
    }

    private IEnumerator Co_FightCoolTime()
    {
        spriteRenderer.sprite = canShotSprite;
        isFight = true;
        yield return fightCoolTime;
        isFight = false;
        spriteRenderer.sprite = normalSprite;
    }
}
