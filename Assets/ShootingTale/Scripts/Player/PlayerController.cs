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
    public Animator changeAnimator;
    public Animator waveAnimator;

    private float mysize = 0.16f;
    public float maxDistance = 1.09f;

    private int horizontalMultiKey = 0;       // 현재 좌우 방향키 중 몇개의 키가 눌렸는지
    private int verticalMultiKey = 0;       // 현재 상하 방향키 중 몇개의 키가 눌렸는지
    private float horizontalFirstDir = 0f;    // 좌우 방향키 중 먼저 눌린 키 방향
    private float verticalFirstDir = 0f;    // 상하 방향키 중 먼저 눌린 키 방향

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
        Move();
    }

    #region Move
    /// <summary>
    /// 플레이어가 움직이게 해주는 메소드
    /// </summary>
    private void Move()
    {
        var xInput = GetHorizontalAxisRaw(KeyCode.LeftArrow, KeyCode.RightArrow);
        var yInput = GetVerticalAxisRaw(KeyCode.DownArrow, KeyCode.UpArrow);

        Debug.Log("Horizontal: " + xInput);
        Debug.Log("Vertical: " + yInput);

        movement2D.MoveTo(new Vector3(xInput, yInput, 0)); 
    }

    /// <summary>
    /// 새로운 GetHorizontalAxisRaw
    /// </summary>
    /// <param name="k1">왼쪽키</param>
    /// <param name="k2">오른쪽키</param>
    /// <returns></returns>
    private float GetHorizontalAxisRaw(KeyCode k1, KeyCode k2)
    {
        var returnDir = 0f;

        if (Time.timeScale == 0f) return returnDir;

        if (Input.GetKey(k1))
        {
            // 방향을 왼쪽으로 처리 or 방향을 아래쪽으로 처리
            returnDir = -1f;

            // 키를 눌렀음을 체크
            if (Input.GetKeyDown(k1))
            {
                horizontalMultiKey++;
            }

            // 좌우 방향키 중 먼저 눌린 키를 처리 or 위아래 방향키 중 먼저 눌린 키를 처리
            if(horizontalMultiKey == 1)
            {
                horizontalFirstDir = -1;
            }
           
        }

        if (Input.GetKey(k2))
        {
            // 방향을 오른쪽으로 처리 or 방향을 위쪽으로 처리
            returnDir = 1f;

            // 키를 눌렀음을 체크
            if (Input.GetKeyDown(k2))
            {
                horizontalMultiKey++;
            }

            // 좌우 방향키 중 먼저 눌린 키를 처리 or 위아래 방향키 중 먼저 눌린 키를 처리
            if (horizontalMultiKey == 1)
            {
                horizontalFirstDir = 1;
            }
        }

        // 먼저 눌린 방향의 반대 방향으로 이동
        if(horizontalMultiKey == 2)
        {
            returnDir = -horizontalFirstDir;
        }

        // 키 누름 해제
        if(Input.GetKeyUp(k1))
        {
            horizontalMultiKey--;
        }

        if (Input.GetKeyUp(k2))
        {
            horizontalMultiKey--;
        }

        // 초기화
        if (horizontalMultiKey == 0)
        {
            horizontalFirstDir = 0f;
        }

        return returnDir;
    }

    /// <summary>
    /// 새로운 GetVerticalAxisRaw
    /// </summary>
    /// <param name="k1">아래쪽키</param>
    /// <param name="k2">위쪽키</param>
    /// <returns></returns>
    private float GetVerticalAxisRaw(KeyCode k1, KeyCode k2)
    {
        var returnDir = 0f;

        if (Time.timeScale == 0f) return returnDir;

        if (Input.GetKey(k1))
        {
            // 방향을 왼쪽으로 처리 or 방향을 아래쪽으로 처리
            returnDir = -1f;

            // 키를 눌렀음을 체크
            if (Input.GetKeyDown(k1))
            {
                verticalMultiKey++;
            }

            // 좌우 방향키 중 먼저 눌린 키를 처리 or 위아래 방향키 중 먼저 눌린 키를 처리
            if (verticalMultiKey == 1)
            {
                verticalFirstDir = -1;
            }

        }

        if (Input.GetKey(k2))
        {
            // 방향을 오른쪽으로 처리 or 방향을 위쪽으로 처리
            returnDir = 1f;

            // 키를 눌렀음을 체크
            if (Input.GetKeyDown(k2))
            {
                verticalMultiKey++;
            }

            // 좌우 방향키 중 먼저 눌린 키를 처리 or 위아래 방향키 중 먼저 눌린 키를 처리
            if (verticalMultiKey == 1)
            {
                verticalFirstDir = 1;
            }
        }

        // 먼저 눌린 방향의 반대 방향으로 이동
        if (verticalMultiKey == 2)
        {
            returnDir = -verticalFirstDir;
        }

        // 키 누름 해제
        if (Input.GetKeyUp(k1))
        {
            verticalMultiKey--;
        }

        if (Input.GetKeyUp(k2))
        {
            verticalMultiKey--;
        }

        // 초기화
        if (verticalMultiKey == 0)
        {
            verticalFirstDir = 0f;
        }

        return returnDir;
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
        changeAnimator.SetBool("isFight", true);
        spriteRenderer.sprite = canShotSprite;
        waveAnimator.SetBool("isFightWave", true);
        waveAnimator.SetBool("isNormalWave", false);
        isFight = true;
        yield return fightCoolTime;
        isFight = false;
        changeAnimator.SetBool("isFight", false);
        spriteRenderer.sprite = normalSprite;
        waveAnimator.SetBool("isNormalWave", true);
        waveAnimator.SetBool("isFightWave", false);
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
