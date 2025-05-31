using System.Collections;
using UnityEngine;

public class SpawnSign : MonoBehaviour
{
    public static SpawnSign Instance;

    public PlayerController playerController;

    public float spawnRate;
    public Transform[] spawnPoints;
    public GameObject[] signPrefabs;

    [HideInInspector] public bool isSummon;
    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        isSummon = false;
        _waitForSeconds = new WaitForSeconds(spawnRate);
    }

    private void Update()
    {
        if (!isSummon && GameManager.Instance.progressType == ProgressType.Start) SummonSign();
    }

    public void DeleteSignAll()
    {
        foreach (var point in spawnPoints) Destroy(point.GetChild(0).gameObject);
    }

    public void SummonSign()
    {
        StartCoroutine(Co_SpawnRate());
    }

    private IEnumerator Co_SpawnRate()
    {
        isSummon = true;
        yield return _waitForSeconds;
        int randomIndex = Random.Range(0, spawnPoints.Length);
        int randomSign = Random.Range(0, signPrefabs.Length);
        var obj = Instantiate(signPrefabs[randomSign], spawnPoints[randomIndex].transform);

        foreach (var signPrefab in signPrefabs)
        foreach (var point in spawnPoints)
        {
            if (signPrefab == obj || point.transform == obj.transform.parent) continue;
            Instantiate(signPrefab, point.transform);
            yield break;
        }
    }
}