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
                // 현재 프리팹과 스폰 포인트가 처음에 선택된 것과 다른지 확인
                if (signPrefabs[i] != obj && spawnPoints[j].transform != obj.transform.parent)
                {
                    // 현재 스폰 포인트와 오브젝트 정보를 로그로 출력
                    Debug.Log($"{spawnPoints[j].gameObject}, {signPrefabs[i].gameObject}");

                    // 새 프리팹을 현재 스폰 포인트에 인스턴스화
                    Instantiate(signPrefabs[i], spawnPoints[j].transform);
                    yield break; // 유효한 조합을 찾았으므로 루프 종료
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
