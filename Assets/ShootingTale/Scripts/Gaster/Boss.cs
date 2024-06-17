using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject boss;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        this.boss = IsActiveBossObject();
    }

    public GameObject IsActiveBossObject()
    {
        foreach (GameObject go in bosses)
        {
            if (go.activeSelf)
            {
                return go;
            }
        }
        return null;
    }
}
