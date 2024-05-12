using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player Movement
    private Movement2D movement2D;

    [Header("Player Gun")]
    public GunEnums.EGunType eGunType;
    public Gun bullet;

    private WaitForSeconds shotCoolTime;
    private WaitForSeconds fightCoolTime;
    private readonly float attackRate = 0.2f;
    private readonly float fightRate = 10.0f;

    private bool isShot = false;
    [HideInInspector] public bool isFight = false;

    [Header("Player Hit")]
    public SpriteRenderer spriteRenderer;
    public Sprite normalSprite;
    public Sprite canShotSprite;

    public LayerMask hitSign;

    public SpawnSign spawnSign;
    public Animator animator;

    private float mysize = 0.16f;
    public float maxDistance = 1.09f;

    public void Init(Gun bullet)
    {
        this.bullet = bullet;
        this.bullet.transform.SetParent(this.transform);
        this.bullet.transform.localPosition = Vector3.zero;
        this.spriteRenderer.sprite = this.normalSprite;
    }

    public void Init()
    {
        movement2D = GetComponent<Movement2D>();

        shotCoolTime = new WaitForSeconds(attackRate);
        fightCoolTime = new WaitForSeconds(fightRate);
    }

    private void Awake()
    {
        GameMain gameMain = GameObject.FindObjectOfType<GameMain>();
        gameMain.Init(this.eGunType);

        this.Init();
    }

    private void Update()
    {
        ShootBullet();
        HitSign();
    }

    private void FixedUpdate()
    {
        Move();
    }

    #region Move
    /// <summary>
    /// 플레이어가 움직이게 해주는 메소드
    /// </summary>
    private void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        movement2D.MoveTo(new Vector3(xInput, yInput, 0)); 
    }
    #endregion

    #region Shot
    /// <summary>
    /// 플레이어가 총을 발사하게 해주는 메소드
    /// </summary>
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
    #endregion

    #region Fight
    /// <summary>
    /// Sign에 닿았음을 감지하는 메소드
    /// </summary>
    private void HitSign()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, mysize, Vector2.up, maxDistance, hitSign);
        if(hit.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.Z) && !isFight)
            {
                if (hit.collider.CompareTag("FightSign"))
                {
                    StartCoroutine(Co_FightCoolTime());
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    private IEnumerator Co_FightCoolTime()
    {
        animator.SetBool("isFight", true);
        spriteRenderer.sprite = canShotSprite;
        isFight = true;
        yield return fightCoolTime;
        isFight = false;
        spriteRenderer.sprite = normalSprite;
        animator.SetBool("isFight", false);
        spawnSign.isSummon = false;
        spawnSign.SummonSign();
    }

    #endregion

    /// <summary>
    /// 플레이어 Raycast 범위 확인용 OnDrawGizmos
    /// </summary>
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(transform.position, maxDistance);
    //}
}
