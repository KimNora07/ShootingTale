//System
using System.Collections;
using System.Collections.Generic;

//UnityEngine
using UnityEngine;

public class AttackLine : MonoBehaviour
{
    public static AttackLine instance;

    [SerializeField] private GameObject attackLinePrefab;
    [SerializeField] private GameObject bulletPrefab;

    public GameObject parent;

    private void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        for(int i = 0; i < parent.transform.childCount; i++)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }
    }

    public void StartAttack(AttackType attackType)
    {
        StartCoroutine(attackType.ToString());
    }

    public void StopAttack(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());
    }

    public IEnumerator SniperAttack()
    {
        while (true)
        {
            float positionY = Random.Range(-9.7f, -1.8f);
            GameObject line = Instantiate(attackLinePrefab, new Vector3(0, positionY, 0), Quaternion.identity);
            line.transform.SetParent(parent.transform);

            yield return new WaitForSeconds(0.5f);

            Destroy(line);

            int index = Random.Range(0, 2);

            if (index == 0)
            {
                Vector3 bulletPosition = new Vector3(-20, positionY, 0);
                GameObject go = Instantiate(bulletPrefab, bulletPosition, bulletPrefab.transform.localRotation);
                go.GetComponent<SniperBullet>().SniperBulletPosition = SniperBulletPosition.Left;
                go.GetComponent<SniperBullet>().spriteRenderer.flipX = false;
            }
            else if(index == 1)
            {
                Vector3 bulletPosition = new Vector3(20, positionY, 0);
                GameObject go = Instantiate(bulletPrefab, bulletPosition, bulletPrefab.transform.localRotation);
                go.GetComponent<SniperBullet>().SniperBulletPosition = SniperBulletPosition.Right;
                go.GetComponent<SniperBullet>().spriteRenderer.flipX = true;
            }

            float spawnTime = Random.Range(0.5f, 1f);

            yield return null;
            //yield return new WaitForSeconds(0.1f);
        }
    }
}
