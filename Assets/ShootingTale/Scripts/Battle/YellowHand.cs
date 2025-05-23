using System.Collections;
using UnityEngine;

public class YellowHand : MonoBehaviour
{
    public static YellowHand instance;

    public Animator animator;
    public ActiveBossType bossType;

    private bool isSelectedPattern;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (gameObject.GetComponent<HandInfo>().hp <= 0 && gameObject.activeSelf) Die();

        if (GameManager.Instance.progressType == ProgressType.Start && !isSelectedPattern)
            StartCoroutine(RandomPattern());
    }

    private void DetectPattern()
    {
        YellowHandPattern.Instance.StartAttack(AttackType.DetectAttack);
    }

    private void StopDetectPattern()
    {
        YellowHandPattern.Instance.StopAttack(AttackType.DetectAttack);
    }

    private void SniperPattern()
    {
        AttackLine.instance.StartAttack(AttackType.SniperAttack);
    }

    private void StopSniperPattern()
    {
        AttackLine.instance.StopAttack(AttackType.SniperAttack);
    }

    private IEnumerator RandomPattern()
    {
        isSelectedPattern = true;

        int randomIndex = Random.Range(0, 2);

        switch (randomIndex)
        {
            case 0:
                AttackLine.instance.Init();
                StopSniperPattern();
                DetectPattern();
                break;
            case 1:
                AttackLine.instance.Init();
                StopDetectPattern();
                SniperPattern();
                break;
            case 2:
                AttackLine.instance.Init();
                StopSniperPattern();
                StopDetectPattern();
                DetectPattern();
                SniperPattern();
                break;
        }

        int randomTime = Random.Range(5, 10);
        yield return new WaitForSeconds(randomTime);
        isSelectedPattern = false;
    }

    private void Die()
    {
        Boss.Instance.isDie = true;
        gameObject.GetComponent<HandInfo>().hp = 0;
        animator.SetBool("IsDie", true);
    }

    public void EndDieAnimation()
    {
        AttackLine.instance.Init();

        gameObject.SetActive(false);
        Boss.Instance.nextBoss.SetActive(true);
        Boss.Instance.isDie = false;
        Boss.Instance.Init();
    }
}