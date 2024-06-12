using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private static PlayerInfo instance;
    public static PlayerInfo Instance { get { return instance; } }

    public int hp = 20;

    public bool isInvincibility = false;

    private void Awake()
    {
        instance = this; 
    }

    private void Update()
    {
        Die();
    }

    public void TakeDamage(int Damage)
    {
        hp -= Damage;
        Debug.Log($"{Damage}의 데미지를 입었습니다, 남은 HP: {this.hp}");
        StartCoroutine(InvincibilityJudgement());
    }

    /// <summary>
    /// 피격당했을 시 1초 무적판정
    /// </summary>
    /// <returns></returns>
    private IEnumerator InvincibilityJudgement()
    {
        isInvincibility = true;
        yield return new WaitForSeconds( 1 );
        isInvincibility = false;
        yield return null;
    }

    public void Die()
    {
        if(hp <= 0)
        {
            hp = 0;
            Debug.Log("플레이어 사망");
        }
    }
}
