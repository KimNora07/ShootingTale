using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { DetectAttack = 0, SniperAttack = 1};

public class YellowHandPattern : MonoBehaviour
{
    public static YellowHandPattern Instance { get; private set; }

    public GameObject bulletPrefab = null; // ź�� ������Ʈ
    public Transform[] points;
    public Transform target;
    public Transform saveTarget;

    private void Awake()
    {
        Instance = this;
    }

    public void StartAttack(AttackType attackType)
    {
        StartCoroutine(attackType.ToString());
    }

    public void StopAttack(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());
    }
    
    public IEnumerator DetectAttack()
    {
        float attackRate = 0.25f;

        while (true)
        {
            GameObject go = Instantiate(bulletPrefab);
            int index = Random.Range(0, points.Length);
            go.transform.position = points[index].position;

            yield return new WaitForSeconds(attackRate);
        }
    }
}
