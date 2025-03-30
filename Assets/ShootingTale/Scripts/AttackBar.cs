//System
using System.Collections;
using System.Collections.Generic;

//UnityEngine
using UnityEngine;

public class AttackBar : MonoBehaviour
{
    [SerializeField] private RectTransform line;
    [SerializeField] private Transform startPos;

    public Player player;
    public PlayerController controller;

    public bool isSelect;

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        isSelect = false;
        line.transform.position = startPos.position;
    }

    public void SelectedAttack()
    {
        StartCoroutine(Co_SelectedAttack());
    }

    private IEnumerator Co_SelectedAttack()
    {
        isSelect = true;

        if (line.transform.localPosition.x < -68.5f)
        {
            int randomIndex = Random.Range(0, player.playerLowAtk.Length);
            player.currentPlayerAtk = player.playerLowAtk[randomIndex];
        }
        else if (line.transform.localPosition.x < -36.5f)
        {
            int randomIndex = Random.Range(2, player.playerLowAtk.Length);
            player.currentPlayerAtk = player.playerLowAtk[randomIndex];
        }
        else if (line.transform.localPosition.x < -13f)
        {
            int randomIndex = Random.Range(0, player.playerMediumAtk.Length);
            player.currentPlayerAtk = player.playerMediumAtk[randomIndex];
        }
        else if (line.transform.localPosition.x < -1.5f)
        {
            int randomIndex = Random.Range(2, player.playerMediumAtk.Length);
            player.currentPlayerAtk = player.playerMediumAtk[randomIndex];
        }
        else if (line.transform.localPosition.x < 1.5f)
        {
            int randomIndex = Random.Range(0, player.playerHighAtk.Length);
            player.currentPlayerAtk = player.playerHighAtk[randomIndex];
        }
        else if (line.transform.localPosition.x < 13f)
        {
            int randomIndex = Random.Range(2, player.playerMediumAtk.Length);
            player.currentPlayerAtk = player.playerMediumAtk[randomIndex];
        }
        else if (line.transform.localPosition.x < 36.5f)
        {
            int randomIndex = Random.Range(0, player.playerMediumAtk.Length);
            player.currentPlayerAtk = player.playerMediumAtk[randomIndex];
        }
        else if (line.transform.localPosition.x < 68.5f)
        {
            int randomIndex = Random.Range(2, player.playerLowAtk.Length);
            player.currentPlayerAtk = player.playerLowAtk[randomIndex];
        }
        else
        {
            int randomIndex = Random.Range(0, player.playerLowAtk.Length);
            player.currentPlayerAtk = player.playerLowAtk[randomIndex];
        }

        yield return new WaitForSeconds(1f);

        isSelect = false;

        controller.FightCoolTime();
        this.gameObject.SetActive(false);

    }

}
