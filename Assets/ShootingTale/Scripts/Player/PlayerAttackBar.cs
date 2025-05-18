//System
using System.Collections;

//UnityEngine
using UnityEngine;

public class PlayerAttackBar : MonoBehaviour
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

        switch (line.transform.localPosition.x)
        {
            case < -68.5f:
            {
                int randomIndex = Random.Range(0, player.playerLowAtk.Length);
                player.currentPlayerAtk = player.playerLowAtk[randomIndex];
                break;
            }
            case < -36.5f:
            {
                int randomIndex = Random.Range(2, player.playerLowAtk.Length);
                player.currentPlayerAtk = player.playerLowAtk[randomIndex];
                break;
            }
            case < -13f:
            {
                int randomIndex = Random.Range(0, player.playerMediumAtk.Length);
                player.currentPlayerAtk = player.playerMediumAtk[randomIndex];
                break;
            }
            case < -1.5f:
            {
                int randomIndex = Random.Range(2, player.playerMediumAtk.Length);
                player.currentPlayerAtk = player.playerMediumAtk[randomIndex];
                break;
            }
            case < 1.5f:
            {
                int randomIndex = Random.Range(0, player.playerHighAtk.Length);
                player.currentPlayerAtk = player.playerHighAtk[randomIndex];
                break;
            }
            case < 13f:
            {
                int randomIndex = Random.Range(2, player.playerMediumAtk.Length);
                player.currentPlayerAtk = player.playerMediumAtk[randomIndex];
                break;
            }
            case < 36.5f:
            {
                int randomIndex = Random.Range(0, player.playerMediumAtk.Length);
                player.currentPlayerAtk = player.playerMediumAtk[randomIndex];
                break;
            }
            case < 68.5f:
            {
                int randomIndex = Random.Range(2, player.playerLowAtk.Length);
                player.currentPlayerAtk = player.playerLowAtk[randomIndex];
                break;
            }
            default:
            {
                int randomIndex = Random.Range(0, player.playerLowAtk.Length);
                player.currentPlayerAtk = player.playerLowAtk[randomIndex];
                break;
            }
        }

        yield return new WaitForSeconds(1f);

        isSelect = false;

        controller.FightCoolTime();
        this.gameObject.SetActive(false);

    }

}
