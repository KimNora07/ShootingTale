using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private static PlayerInfo instance;
    public static PlayerInfo Instance { get { return instance; } }

    private Animator animator;

    public int hp = 20;

    public bool isInvincibility = false;
    private bool isDie = false;

    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //if (hp > 0)
        //{
        //    UIManager.Instance.playerHpText.text = hp.ToString();
        //}
        //else
        //{
        //    UIManager.Instance.playerHpText.text = "0";
        //}
        Die();
    }

    public void TakeDamage(int Damage)
    {
        if (hp > 0)
        {
            hp -= Damage;
            Debug.Log($"{Damage}의 데미지를 입었습니다, 남은 HP: {this.hp}");
        }
        
        StartCoroutine(InvincibilityJudgement());
    }

    /// <summary>
    /// 피격당했을 시 1초 무적판정
    /// </summary>
    /// <returns></returns>
    private IEnumerator InvincibilityJudgement()
    {
        isInvincibility = true;
        animator.SetBool("isInvincibillity", isInvincibility);
        yield return new WaitForSeconds( 1 );
        isInvincibility = false;
        animator.SetBool("isInvincibillity", isInvincibility);
        yield return null;
    }

    public void Die()
    {
        if(hp <= 0 && !isDie)
        {
            hp = 0;
            isDie = true;
            animator.SetBool("isDie", true);
            GameMain.instance.progressType = ProgressType.Die;
            StartCoroutine(ReturnToSplash());
        }
    }

    private IEnumerator ReturnToSplash()
    {
        yield return new WaitForSeconds( 2f );
        LoadingManager.LoadScene("00_Splash", "99_Loading");
    }
}
