using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private static PlayerInfo instance;
    public static PlayerInfo Instance { get { return instance; } }

    public int hp = 20;

    private void Awake()
    {
        instance = this; 
    }

    public void TakeDamage(int Damage)
    {
        hp -= Damage;
        Debug.Log($"{Damage}�� �������� �Ծ����ϴ�, ���� HP: {this.hp}");
    }
}