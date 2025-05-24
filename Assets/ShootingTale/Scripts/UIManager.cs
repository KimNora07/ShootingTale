using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text playerHpText;
    public Text bossHpText;

    public GameObject ui;
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        bossHpText.text = $"{Boss.Instance.bossName}'s HP: {Boss.Instance.boss.GetComponent<HandInfo>().hp}";
        playerHpText.text = PlayerInfo.Instance.hp.ToString();
    }

    private void Init()
    {
        playerHpText.text = PlayerInfo.Instance.hp.ToString();
    }
}