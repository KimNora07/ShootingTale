//System
using System.Collections;
using System.Collections.Generic;

//UnityEngine
using UnityEngine;
using UnityEngine.UI;

public class DamageTextController : MonoBehaviour
{
    public static DamageTextController Instance;

    private void Awake()
    {
        Instance = this;
        uiCamera = canvas.worldCamera;
    }

    public Camera uiCamera;
    public Canvas canvas;
    public GameObject dmgTxt;

    public void CreateDamageText(Vector3 hitPoint, int hitDamage)
    {
        GameObject damageText = Instantiate(dmgTxt, hitPoint, Quaternion.identity, canvas.transform);
        damageText.GetComponent<Text>().text = $"-{hitDamage}";
    }
}
