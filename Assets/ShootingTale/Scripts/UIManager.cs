using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TMP_Text playerHpText;
    public Text bossHpText;

    public GameObject ui;

    private void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        playerHpText.text = PlayerInfo.Instance.hp.ToString();
    }

    private void Update()
    {
        bossHpText.text = $"{Boss.Instance.bossName}'s HP: {Boss.Instance.boss.GetComponent<HandInfo>().hp}";
        playerHpText.text = PlayerInfo.Instance.hp.ToString();
    }
}
