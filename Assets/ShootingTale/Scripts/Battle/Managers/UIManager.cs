using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text bossHpText;

    public GameObject ui;
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //Init();
    }

    private void Update()
    {
        bossHpText.text = $"{Boss.Instance.bossName}'s HP: {Boss.Instance.boss.GetComponent<HandInfo>().hp}";
    }
}