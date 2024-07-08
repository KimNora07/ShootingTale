using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSign : MonoBehaviour
{
    public PlayerController playerController;

    public float spawnRate;
    private WaitForSeconds waitForSeconds;
    public Transform[] spawnPoints;
    public GameObject signPrefab;

    [HideInInspector] public bool isSummon;

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

    public void SummonSign()
    {
        StartCoroutine(Co_SpawnRate());
    }

    private IEnumerator Co_SpawnRate()
    {
        isSummon = true;
        yield return waitForSeconds;
        int index = Random.Range(0, spawnPoints.Length);
        Instantiate(signPrefab, spawnPoints[index].transform);
    }
}
