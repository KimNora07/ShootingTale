using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Box
{
    public string bossName;
    public GameObject originalBox;
    public GameObject changedBox;
    public List<GameObject> warningBoxes;
    public List<GameObject> childBoxes;
}

[DisallowMultipleComponent]
public class BossBox : MonoBehaviour
{
    public List<Box> bossBoxes;
}
