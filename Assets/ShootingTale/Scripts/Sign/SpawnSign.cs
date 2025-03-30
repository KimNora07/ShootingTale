using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSign : MonoBehaviour
{
    public static SpawnSign instance;


    public PlayerController playerController;

    public float spawnRate;
    private WaitForSeconds waitForSeconds;
    public Transform[] spawnPoints;
    public GameObject[] signPrefabs;

    [HideInInspector] public bool isSummon;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        isSummon = false;
        waitForSeconds = new WaitForSeconds(spawnRate);
    }

    private void Update()
    {
        if (!isSummon && GameMain.instance.progressType == ProgressType.Start)
        {
            SummonSign();
        }
        
    }

    public void DeleteSignAll()
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            Destroy(spawnPoints[i].GetChild(0).gameObject);
        }
    }

    public void SummonSign()
    {
        StartCoroutine(Co_SpawnRate());
    }

    private IEnumerator Co_SpawnRate()
    {
        isSummon = true;
        yield return waitForSeconds;
        int randomIndex = Random.Range(0, spawnPoints.Length);
        int randomSign = Random.Range(0, signPrefabs.Length);
        GameObject obj = Instantiate(signPrefabs[randomSign], spawnPoints[randomIndex].transform);

        for (int i = 0; i < signPrefabs.Length; i++)
        {
            for (int j = 0; j < spawnPoints.Length; j++)
            {
                // ���� �����հ� ���� ����Ʈ�� ó���� ���õ� �Ͱ� �ٸ��� Ȯ��
                if (signPrefabs[i] != obj && spawnPoints[j].transform != obj.transform.parent)
                {
                    // ���� ���� ����Ʈ�� ������Ʈ ������ �α׷� ���
                    Debug.Log($"{spawnPoints[j].gameObject}, {signPrefabs[i].gameObject}");

                    // �� �������� ���� ���� ����Ʈ�� �ν��Ͻ�ȭ
                    Instantiate(signPrefabs[i], spawnPoints[j].transform);
                    yield break; // ��ȿ�� ������ ã�����Ƿ� ���� ����
                }
            }
        }

        //for (int i = 0; i < signPrefabs.Length; i++)
        //{
        //    for (int j = 0; j < spawnPoints.Length; j++)
        //    {
        //        if (obj != signPrefabs[i] && obj.transform.parent.gameObject != spawnPoints[j].gameObject)
        //        {
        //            Debug.Log($"{obj.transform.parent.gameObject}, {spawnPoints[j].gameObject}");
        //            Instantiate(signPrefabs[j], spawnPoints[i].transform);
        //            yield break;
        //        }
        //    }
        //}
    }
}
