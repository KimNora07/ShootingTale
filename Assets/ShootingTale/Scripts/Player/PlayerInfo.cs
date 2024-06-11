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
        Debug.Log($"{Damage}의 데미지를 입었습니다, 남은 HP: {this.hp}");
    }
}
