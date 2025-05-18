using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private static PlayerInfo instance;
    public static PlayerInfo Instance { get { return instance; } }

    private Animator animator;

    public int hp = 20;

    public bool isInvincibility = false;
    private bool isDie = false;

    private void Awake()
    {
        instance = this; 
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
        if (hp > 0)
        {
            hp -= Damage;
        }
        
        StartCoroutine(InvincibilityJudgement());
    }
    
    private IEnumerator InvincibilityJudgement()
    {
        isInvincibility = true;
        animator.SetBool(ConstantsManager.Invincibility, isInvincibility);
        yield return new WaitForSeconds( 1 );
        isInvincibility = false;
        animator.SetBool(ConstantsManager.Invincibility, isInvincibility);
        yield return null;
    }

    private void Die()
    {
        if(hp <= 0 && !isDie)
        {
            hp = 0;
            isDie = true;
            animator.SetBool(ConstantsManager.Die, true);
            GameManager.Instance.progressType = ProgressType.Die;
            StartCoroutine(ReturnToSplash());
        }
    }

    private static IEnumerator ReturnToSplash()
    {
        yield return new WaitForSeconds( 2f );
        LoadingManager.LoadScene("00_Splash", "99_Loading");
    }
}
