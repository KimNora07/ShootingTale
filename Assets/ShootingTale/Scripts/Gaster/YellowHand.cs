using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowHand : MonoBehaviour
{
    public static YellowHand instance;

    public Animator animator;
    public ActiveBossType bossType;

    private bool isSelectedPattern = false;

    
    // 탄알이 플레이어 방향으로 이동을 정해주고, 발사한다
    //탄막 오브젝트, 속도, 생성할 위치

    //오브젝트 풀링
    //생성된 탄막 오브젝트 발사 방향 지정(플레이어 방향으로 지정)

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(this.gameObject.GetComponent<HandInfo>().hp <= 0 && this.gameObject.activeSelf)
        {
            Die();
        }

        if(GameMain.instance.progressType == ProgressType.Start && !isSelectedPattern)
        {
            StartCoroutine(RandomPattern());
        }
    }

    public void DetectPattern()
    {
        YellowHandPattern.Instance.StartAttack(AttackType.DetectAttack);
    }

    public void StopDetectPattern()
    {
        YellowHandPattern.Instance.StopAttack(AttackType.DetectAttack);
    }

    public void SniperPattern()
    {
        AttackLine.instance.StartAttack(AttackType.SniperAttack);
    }

    public void StopSniperPattern()
    {
        AttackLine.instance.StopAttack(AttackType.SniperAttack);
    }

    private IEnumerator RandomPattern()
    {
        isSelectedPattern = true;

        int randomIndex = Random.Range(0, 2);

        switch (randomIndex)
        {
            case 0: AttackLine.instance.Init(); StopSniperPattern(); DetectPattern(); break;
            case 1: AttackLine.instance.Init(); StopDetectPattern(); SniperPattern(); break;
            case 2: AttackLine.instance.Init(); StopSniperPattern(); StopDetectPattern(); DetectPattern(); SniperPattern(); break;
        }

        int randomTime = Random.Range(5, 10);
        yield return new WaitForSeconds(randomTime);
        isSelectedPattern = false;
    }

    public void Die()
    {
        Boss.Instance.isDie = true;
        this.gameObject.GetComponent<HandInfo>().hp = 0;
        animator.SetBool("IsDie", true);
    }

    public void EndDieAnimation()
    {
        AttackLine.instance.Init();

        this.gameObject.SetActive(false);
        Boss.Instance.nextBoss.SetActive(true);
        Boss.Instance.isDie = false;
        Boss.Instance.Init();
    }
}
