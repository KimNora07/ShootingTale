using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowHand : MonoBehaviour
{
    public static YellowHand instance;
    // ź���� �÷��̾� �������� �̵��� �����ְ�, �߻��Ѵ�
    //ź�� ������Ʈ, �ӵ�, ������ ��ġ

    //������Ʈ Ǯ��
    //������ ź�� ������Ʈ �߻� ���� ����(�÷��̾� �������� ����)

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