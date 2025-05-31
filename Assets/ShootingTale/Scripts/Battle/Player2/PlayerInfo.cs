using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int hp = 20;

    public bool isInvincibility;

    private Animator animator;
    private bool isDie;
    public static PlayerInfo Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Die();
    }

    public void TakeDamage(int Damage)
    {
        if (hp > 0) hp -= Damage;

        StartCoroutine(InvincibilityJudgement());
    }

    private IEnumerator InvincibilityJudgement()
    {
        isInvincibility = true;
        animator.SetBool(ConstantsManager.Invincibility, isInvincibility);
        yield return new WaitForSeconds(1);
        isInvincibility = false;
        animator.SetBool(ConstantsManager.Invincibility, isInvincibility);
        yield return null;
    }

    private void Die()
    {
        if (hp <= 0 && !isDie)
        {
            hp = 0;
            isDie = true;
            animator.SetBool(ConstantsManager.Die, true);
            GameManager.Instance.progressType = ProgressType.Die;
            StartCoroutine(ReturnToSplash());
        }
    }

    private IEnumerator ReturnToSplash()
    {
        yield return new WaitForSeconds(2f);
        SceneLoader.LoadSceneAsync(this, SceneName.SplashScene);
    }
}