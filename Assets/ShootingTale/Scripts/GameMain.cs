using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameMain : MonoBehaviour
{
    public static GameMain instance;

    public GameObject[] gunPrefabs;
    public PlayerController playerController;

    public ProgressType progressType;

    private void Awake()
    {
        instance = this;
        progressType = ProgressType.Wait;
    }

    private void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBGM(AudioManager.Instance.battleBGM);
        }
    }

    public void Init(GunEnums.EGunType selectedGunType)
    {
        Gun gun = this.CreateGun(selectedGunType);
        this.playerController.Init(gun);
        
    }

    private Gun CreateGun(GunEnums.EGunType gunType)
    {
        int index = (int)gunType;
        GameObject prefab = this.gunPrefabs[index];
        GameObject go = Instantiate(prefab);
        Gun gun = go.GetComponent<Gun>();
        return gun;
    }

    
}
