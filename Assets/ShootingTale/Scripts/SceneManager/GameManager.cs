using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] gunPrefabs;
    public PlayerController playerController;

    public ProgressType progressType;

    private void Awake()
    {
        Instance = this;
        progressType = ProgressType.Wait;
    }

    private void Start()
    {
        if (AudioManager.Instance)
            AudioManager.Instance.PlayBGM(AudioManager.Instance.battleBGM);
    }

    public void Init(GunEnums.EGunType selectedGunType)
    {
        var gun = CreateGun(selectedGunType);
        playerController.Init(gun);
    }

    private Gun CreateGun(GunEnums.EGunType gunType)
    {
        var index = (int)gunType;
        var prefab = gunPrefabs[index];
        var go = Instantiate(prefab);
        var gun = go.GetComponent<Gun>();
        return gun;
    }
}