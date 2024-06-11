using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowHand : MonoBehaviour
{
    public static YellowHand instance;
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
        if(Input.GetMouseButtonDown(0))
        {
            SummonBullet();
        }      
    }

    public void SummonBullet()
    {
        YellowHandPattern.Instance.StartAttack(AttackType.DetectAttack);
    }
}
