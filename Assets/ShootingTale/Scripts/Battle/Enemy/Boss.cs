using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum ActiveBossType
{
    RedHand = 0,
    OrangeHand,
    YellowHand,
    GreenHand,
    CyanHand,
    BlueHand,
    PurpleHand
}

public class Boss : MonoBehaviour
{
    public static Boss Instance;

    public List<GameObject> bosses;

    public bool isDie;

    public GameObject boss;
    public string bossName;

    public Volume volume;

    public GameObject nextBoss;

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
        boss = IsActiveBossObject();
        bossName = boss.name;
        // UIManager.Instance.bossHpText.text =
        //     $"{Instance.bossName}'s HP: {Instance.boss.gameObject.GetComponent<HandInfo>().hp}";
        // BossColor();

        nextBoss = FindNextBossObject();
    }

    public GameObject IsActiveBossObject()
    {
        foreach (var go in bosses)
            if (go.activeSelf)
                return go;

        return null;
    }

    public GameObject FindNextBossObject()
    {
        var nextIndex = 0;

        for (var i = 0; i < bosses.Count; i++)
            if (bosses[i].activeSelf)
            {
                nextIndex = i + 1;
                break;
            }

        var go = bosses[nextIndex];

        return go;
    }

    public void BossColor()
    {
        Bloom bloom;

        if (volume.profile.TryGet(out bloom)) bloom.tint.value = boss.GetComponent<HandInfo>().handColor;
    }
}