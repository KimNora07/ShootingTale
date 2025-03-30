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

    public Sprite testSprite;

    public LayerMask hitSign;

    public SpawnSign spawnSign;
    public Animator changeAnimator;
    public Animator waveAnimator;

    private float mysize = 0.16f;
    public float maxDistance = 1.09f;

    private int horizontalMultiKey = 0;       // ���� �¿� ����Ű �� ��� Ű�� ���ȴ���
    private int verticalMultiKey = 0;       // ���� ���� ����Ű �� ��� Ű�� ���ȴ���
    private float horizontalFirstDir = 0f;    // �¿� ����Ű �� ���� ���� Ű ����
    private float verticalFirstDir = 0f;    // ���� ����Ű �� ���� ���� Ű ����

    public GameObject attackBar;

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
        if (GameMain.instance.progressType == ProgressType.Die) return;

        ShootBullet();
        HitSign();
        Move();
    }

    #region Move
    /// <summary>
    /// �÷��̾ �����̰� ���ִ� �޼ҵ�
    /// </summary>
    private void Move()
    {
        //var xInput = GetHorizontalAxisRaw(KeyCode.LeftArrow, KeyCode.RightArrow);
        //var yInput = GetVerticalAxisRaw(KeyCode.DownArrow, KeyCode.UpArrow);

        var xx = Input.GetAxisRaw("Horizontal");
        var yy = Input.GetAxisRaw("Vertical");

        //Debug.Log("Horizontal: " + xInput);
        //Debug.Log("Vertical: " + yInput);

        movement2D.MoveTo(new Vector3(xx, yy, 0)); 
    }

    /// <summary>
    /// ���ο� GetHorizontalAxisRaw
    /// </summary>
    /// <param name="k1">����Ű</param>
    /// <param name="k2">������Ű</param>
    /// <returns></returns>
    private float GetHorizontalAxisRaw(KeyCode k1, KeyCode k2)
    {
        var returnDir = 0f;

        if (Time.timeScale == 0f) return returnDir;

        if (Input.GetKey(k1))
        {
            // ������ �������� ó�� or ������ �Ʒ������� ó��
            returnDir = -1f;

            // Ű�� �������� üũ
            if (Input.GetKeyDown(k1))
            {
                horizontalMultiKey++;
            }

            // �¿� ����Ű �� ���� ���� Ű�� ó�� or ���Ʒ� ����Ű �� ���� ���� Ű�� ó��
            if(horizontalMultiKey == 1)
            {
                horizontalFirstDir = -1;
            }

            Debug.Log($"{k1}, {k2}");
           
        }

        if (Input.GetKey(k2))
        {
            // ������ ���������� ó�� or ������ �������� ó��
            returnDir = 1f;

            // Ű�� �������� üũ
            if (Input.GetKeyDown(k2))
            {
                horizontalMultiKey++;
            }

            // �¿� ����Ű �� ���� ���� Ű�� ó�� or ���Ʒ� ����Ű �� ���� ���� Ű�� ó��
            if (horizontalMultiKey == 1)
            {
                horizontalFirstDir = 1;
            }
        }

        // ���� ���� ������ �ݴ� �������� �̵�
        if(horizontalMultiKey == 2)
        {
            returnDir = -horizontalFirstDir;
        }

        // Ű ���� ����
        if(Input.GetKeyUp(k1))
        {
            horizontalMultiKey--;
        }

        if (Input.GetKeyUp(k2))
        {
            horizontalMultiKey--;
        }

        // �ʱ�ȭ
        if (horizontalMultiKey == 0)
        {
            horizontalFirstDir = 0f;
        }

        return returnDir;
    }

    /// <summary>
    /// ���ο� GetVerticalAxisRaw
    /// </summary>
    /// <param name="k1">�Ʒ���Ű</param>
    /// <param name="k2">����Ű</param>
    /// <returns></returns>
    private float GetVerticalAxisRaw(KeyCode k1, KeyCode k2)
    {
        var returnDir = 0f;

        if (Time.timeScale == 0f) return returnDir;

        if (Input.GetKey(k1))
        {
            // ������ �������� ó�� or ������ �Ʒ������� ó��
            returnDir = -1f;

            // Ű�� �������� üũ
            if (Input.GetKeyDown(k1))
            {
                verticalMultiKey++;
            }

            // �¿� ����Ű �� ���� ���� Ű�� ó�� or ���Ʒ� ����Ű �� ���� ���� Ű�� ó��
            if (verticalMultiKey == 1)
            {
                verticalFirstDir = -1;
            }

        }

        if (Input.GetKey(k2))
        {
            // ������ ���������� ó�� or ������ �������� ó��
            returnDir = 1f;

            // Ű�� �������� üũ
            if (Input.GetKeyDown(k2))
            {
                verticalMultiKey++;
            }

            // �¿� ����Ű �� ���� ���� Ű�� ó�� or ���Ʒ� ����Ű �� ���� ���� Ű�� ó��
            if (verticalMultiKey == 1)
            {
                verticalFirstDir = 1;
            }
        }

        // ���� ���� ������ �ݴ� �������� �̵�
        if (verticalMultiKey == 2)
        {
            returnDir = -verticalFirstDir;
        }

        // Ű ���� ����
        if (Input.GetKeyUp(k1))
        {
            verticalMultiKey--;
        }

        if (Input.GetKeyUp(k2))
        {
            verticalMultiKey--;
        }

        // �ʱ�ȭ
        if (verticalMultiKey == 0)
        {
            verticalFirstDir = 0f;
        }

        return returnDir;
    }
    #endregion

    #region Shot
    /// <summary>
    /// �÷��̾ ���� �߻��ϰ� ���ִ� �޼ҵ�
    /// </summary>
    private void ShootBullet()
     {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!isShot && isFight)
            {
                this.bullet.Shoot();
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlaySfx(AudioManager.Instance.shot);
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
    /// Sign�� ������� �����ϴ� �޼ҵ�
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
                    //StartCoroutine(Co_FightCoolTime());
                    attackBar.SetActive(true);
                    SpawnSign.instance.DeleteSignAll();
                }

                if (hit.collider.CompareTag("StartSign"))
                {
                    GameMain.instance.progressType = ProgressType.Start;
                    Destroy(hit.collider.gameObject);
                }

                if (hit.collider.CompareTag("HealSign"))
                {
                    PlayerInfo.Instance.hp += 3;
                    HealCoolTime();
                    SpawnSign.instance.DeleteSignAll();
                }
            }
        }
    }

    public void HealCoolTime()
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
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySfx(AudioManager.Instance.wave);

        Debug.Log("Starting fight cool time.");

        changeAnimator.SetBool("isFight", true);
        waveAnimator.SetBool("isFightWave", true);
        waveAnimator.SetBool("isNormalWave", false);

        this.spriteRenderer.sprite = testSprite;
        isFight = true;

        Debug.Log("Sprite changed to canShotSprite, fight mode enabled.");

        yield return fightCoolTime;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySfx(AudioManager.Instance.wave);

        isFight = false;
        changeAnimator.SetBool("isFight", false);
        waveAnimator.SetBool("isNormalWave", true);
        waveAnimator.SetBool("isFightWave", false);

        this.spriteRenderer.sprite = normalSprite;
        Debug.Log("Fight ended, sprite changed to normalSprite.");

        spawnSign.isSummon = false;
        spawnSign.SummonSign();
    }

    #endregion

    /// <summary>
    /// �÷��̾� Raycast ���� Ȯ�ο� OnDrawGizmos
    /// </summary>
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(transform.position, maxDistance);
    //}
}
