using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public GameObject[] gunPrefabs;
    public PlayerController playerController;
    
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
