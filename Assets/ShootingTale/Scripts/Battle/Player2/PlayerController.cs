using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    [Header("Player Gun")] public GunEnums.EGunType eGunType;

    public Gun bullet;
    [HideInInspector] public bool isFight;

    [Header("Player Hit")] public SpriteRenderer spriteRenderer;

    public Sprite normalSprite;
    public Sprite canShotSprite;

    public Sprite testSprite;

    public LayerMask hitSign;

    public SpawnSign spawnSign;
    public Animator changeAnimator;
    public Animator waveAnimator;
    public float maxDistance = 1.09f;

    public GameObject attackBar;
    private readonly float attackRate = 0.2f;
    private readonly float fightRate = 10.0f;
    private WaitForSeconds fightCoolTime;

    private bool isShot;

    private readonly float mysize = 0.16f;

    // Player Movement
    private Rigidbody2D rb;

    private WaitForSeconds shotCoolTime;
    
    private PlayerView view;

    [Obsolete("Obsolete")]
    private void Awake()
    {
        view = FindObjectOfType<PlayerView>();
        var gameMain = FindObjectOfType<GameManager>();
        gameMain.Init(eGunType);

        Init();
    }

    private void Update()
    {
        if (GameManager.Instance.progressType == ProgressType.Die) return;

        ShootBullet();
        HitSign();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Init(Gun bullet)
    {
        this.bullet = bullet;
        this.bullet.transform.SetParent(transform);
        this.bullet.transform.localPosition = Vector3.zero;
        spriteRenderer.sprite = normalSprite;
    }

    private void Init()
    {
        rb = GetComponent<Rigidbody2D>();

        shotCoolTime = new WaitForSeconds(attackRate);
        fightCoolTime = new WaitForSeconds(fightRate);
    }

    #region Move

    private void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        rb.MovePosition(transform.position + new Vector3(xInput, yInput) * (moveSpeed * Time.fixedDeltaTime));
    }

    #endregion

    #region Shot

    private void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            if (!isShot && isFight)
            {
                bullet.Shoot();
                if (AudioManager.Instance)
                    AudioManager.Instance.PlaySfx(AudioManager.Instance.shot);
                StartCoroutine(Co_ShotCoolTime());
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

    private void HitSign()
    {
        var hit = Physics2D.CircleCast(transform.position, mysize, Vector2.up, maxDistance, hitSign);
        if (hit.collider)
            if (Input.GetKeyDown(KeyCode.Z) && !isFight)
            {
                if (hit.collider.CompareTag("FightSign"))
                {
                    view.presenter.SelectedAttackSign();
                    SpawnSign.Instance.DeleteSignAll();
                }

                if (hit.collider.CompareTag("StartSign"))
                {
                    GameManager.Instance.progressType = ProgressType.Start;
                    Destroy(hit.collider.gameObject);
                }

                if (hit.collider.CompareTag("HealSign"))
                {
                    PlayerInfo.Instance.hp += 3;
                    HealCoolTime();
                    SpawnSign.Instance.DeleteSignAll();
                }
            }
    }

    private void HealCoolTime()
    {
        StartCoroutine(Co_HealCoolTime());
    }

    private IEnumerator Co_HealCoolTime()
    {
        yield return fightCoolTime;
        spawnSign.isSummon = false;
        spawnSign.SummonSign();
    }

    public void FightCoolTime()
    {
        StartCoroutine(Co_FightCoolTime());
    }

    private IEnumerator Co_FightCoolTime()
    {
        if (AudioManager.Instance)
            AudioManager.Instance.PlaySfx(AudioManager.Instance.wave);

        changeAnimator.SetBool("isFight", true);
        waveAnimator.SetBool("isFightWave", true);
        waveAnimator.SetBool("isNormalWave", false);

        spriteRenderer.sprite = testSprite;
        isFight = true;

        yield return fightCoolTime;

        if (AudioManager.Instance)
            AudioManager.Instance.PlaySfx(AudioManager.Instance.wave);

        isFight = false;
        changeAnimator.SetBool("isFight", false);
        waveAnimator.SetBool("isNormalWave", true);
        waveAnimator.SetBool("isFightWave", false);

        spriteRenderer.sprite = normalSprite;

        spawnSign.isSummon = false;
        spawnSign.SummonSign();
    }

    #endregion
}