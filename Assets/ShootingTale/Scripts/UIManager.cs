using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TMP_Text playerHpText;

    private void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        playerHpText.text = PlayerInfo.Instance.hp.ToString();
    }
}
