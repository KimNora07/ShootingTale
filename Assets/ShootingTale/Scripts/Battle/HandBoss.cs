using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBoss : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds0_5 = new WaitForSeconds(0.5f);

    [SerializeField] private string bossName;
    [SerializeField] private BossBox bossBox;
    private Box currentBox;

    private int currentPhase = 1;

    private Coroutine phaseRoutine;

    private void Start()
    {
        foreach (var box in bossBox.bossBoxes)
        {
            if (box.bossName.Equals(this.bossName))
            {
                currentBox = box;
            }
        }

        currentBox.originalBox.SetActive(true);
        currentBox.changedBox.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(phaseRoutine != null) StopCoroutine(phaseRoutine);
            phaseRoutine = StartCoroutine(PhaseRoutine());
        }
    }

    private IEnumerator PhaseRoutine()
    {
        yield return _waitForSeconds0_5;

        // 페이즈 값 증가
        currentPhase++;

        if (currentBox == null) yield break;

        // 보스의 박스 변경 주의 표시(페이드 인/아웃)
        foreach (var warningBox in currentBox.warningBoxes)
        {
            StartCoroutine(FadeLoop(warningBox.GetComponent<SpriteRenderer>()));
            yield return _waitForSeconds0_5;
        }

        yield return _waitForSeconds0_5;

        // 보스의 박스 변경
        currentBox.originalBox.SetActive(false);
        currentBox.changedBox.SetActive(true);

        // 중력 변환
        foreach (var boxBody in currentBox.childBoxes)
        {
            boxBody.GetComponent<Rigidbody2D>().simulated = true;
            yield return _waitForSeconds0_5;
        }
    }
    
    private IEnumerator FadeLoop(SpriteRenderer sr)
    {
        while (true) // Loop indefinitely
        {
            // Fade In
            yield return StartCoroutine(FadeSprite(sr, 0f, 1f, 0.5f));
            yield return new WaitForSeconds(0.25f); // Hold fully visible

            // Fade Out
            yield return StartCoroutine(FadeSprite(sr, 1f, 0f, 0.5f));
            yield return new WaitForSeconds(0.25f); // Hold fully invisible
        }
    }

    private IEnumerator FadeSprite(SpriteRenderer sr, float startAlpha, float targetAlpha, float duration)
    {
        float timer = 0f;
        Color currentColor = sr.color;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / duration);
            sr.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null; // Wait for the next frame
        }

        // Ensure the final alpha value is set precisely
        sr.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
    }
}
