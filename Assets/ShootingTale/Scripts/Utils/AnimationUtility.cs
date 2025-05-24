//System

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//UnityEngine

/// <summary>
///     Dotween의 기능을 참고하여 개발
/// </summary>
public static class AnimationUtility
{
    private static IEnumerator Move(RectTransform rect, float duration, float delay, Vector2 toMove,
        Action onPlay = null, Action onComplete = null)
    {
        var startPosition = rect.anchoredPosition;
        yield return new WaitForSeconds(delay);
        onPlay?.Invoke();
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += 0.016f;
            yield return new WaitForSeconds(0.016f);

            float t = Mathf.Clamp01(elapsedTime / duration);
            float easedT = 1 - Mathf.Pow(1 - t, 2);

            rect.anchoredPosition = Vector2.Lerp(startPosition, toMove, easedT);
        }

        onComplete?.Invoke();
    }

    private static IEnumerator FadeIn(Graphic graphic, float duration, float delay, Color color, Action onPlay,
        Action onComplete)
    {
        yield return new WaitForSeconds(delay);
        onPlay?.Invoke();
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += 0.016f;
            yield return new WaitForSeconds(0.016f);

            float t = Mathf.Clamp01(elapsedTime / duration);
            float easedT = 1 - Mathf.Pow(1 - t, 2);

            graphic.color = new Color(color.r, color.g, color.b, easedT);
        }

        onComplete?.Invoke();
    }

    private static IEnumerator FadeOut(Graphic graphic, float duration, float delay, Color color, Action onPlay,
        Action onComplete)
    {
        yield return new WaitForSeconds(delay);
        onPlay?.Invoke();
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += 0.016f;
            yield return new WaitForSeconds(0.016f);

            float t = Mathf.Clamp01(elapsedTime / duration);
            float easedT = 1 - Mathf.Pow(1 - t, 2);

            graphic.color = new Color(color.r, color.g, color.b, 1 - easedT);
        }

        onComplete?.Invoke();
    }

    private static IEnumerator ImageSlide(Image image, float duration, float delay, float start, float end,
        int fillOrigin, Action onPlay, Action onComplete)
    {
        image.fillOrigin = fillOrigin;

        yield return new WaitForSeconds(delay);
        onPlay?.Invoke();
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float easedT = 1 - Mathf.Pow(1 - t, 2);
            image.fillAmount = Mathf.Lerp(start, end, easedT);

            yield return null;
        }

        image.fillAmount = end;
        onComplete?.Invoke();
    }

    private static IEnumerator ScaleX(GameObject obj, float fromSize, float toSize, float duration, float delay,
        Action onPlay, Action onComplete)
    {
        yield return new WaitForSeconds(delay);
        onPlay?.Invoke();
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += 0.016f;
            yield return new WaitForSeconds(0.016f);
            float t = Mathf.Clamp01(elapsedTime / duration);
            float newScaleX = Mathf.Lerp(fromSize, toSize, t);
            obj.transform.localScale = new Vector3(newScaleX, obj.transform.localScale.y, obj.transform.localScale.z);
        }

        onComplete?.Invoke();
    }

    private static IEnumerator ScaleY(GameObject obj, float fromSize, float toSize, float duration, float delay,
        Action onPlay, Action onComplete)
    {
        yield return new WaitForSeconds(delay);
        onPlay?.Invoke();
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += 0.016f;
            yield return new WaitForSeconds(0.016f);
            float t = Mathf.Clamp01(elapsedTime / duration);
            float newScaleY = Mathf.Lerp(fromSize, toSize, t);
            obj.transform.localScale = new Vector3(obj.transform.localScale.x, newScaleY, obj.transform.localScale.z);
        }

        onComplete?.Invoke();
    }

    #region Animations

    /// <summary>
    ///     이동 애니메이션
    /// </summary>
    /// <param name="runner">MonoBehavior를 상속한 클래스</param>
    /// <param name="rect">오브젝트</param>
    /// <param name="duration">지속시간</param>
    /// <param name="delay">지연시간</param>
    /// <param name="toMove">이동할 위치</param>
    /// <param name="onPlay">시작했을 때 실행할 행동</param>
    /// <param name="onComplete">끝났을 때 실행할 행동</param>
    public static void MoveAnimation(MonoBehaviour runner, RectTransform rect, float duration, float delay,
        Vector2 toMove, Action onPlay = null, Action onComplete = null)
    {
        runner.StartCoroutine(Move(rect, duration, delay, toMove, onPlay, onComplete));
    }

    /// <summary>
    ///     페이드 인 애니메이션
    /// </summary>
    /// <param name="runner">MonoBehaviour를 상속한 클래스</param>
    /// <param name="graphic">이미지 또는 텍스트</param>
    /// <param name="duration">지속시간</param>
    /// <param name="delay">지연시간</param>
    /// <param name="color">색상</param>
    /// <param name="onPlay">시작했을 때 실행할 행동</param>
    /// <param name="onComplete">끝났을 때 실행할 행동</param>
    public static void FadeInAnimation(MonoBehaviour runner, Graphic graphic, float duration, float delay, Color color,
        Action onPlay, Action onComplete)
    {
        runner.StartCoroutine(FadeIn(graphic, duration, delay, color, onPlay, onComplete));
    }

    /// <summary>
    ///     페이드 아웃 애니메이션
    /// </summary>
    /// <param name="runner">MonoBehaviour를 상속한 클래스</param>
    /// <param name="graphic">이미지 또는 텍스트</param>
    /// <param name="duration">지속시간</param>
    /// <param name="delay">지연시간</param>
    /// <param name="color">색상</param>
    /// <param name="onPlay">시작했을 때 실행할 행동</param>
    /// <param name="onComplete">끝났을 때 실행할 행동</param>
    public static void FadeOutAnimation(MonoBehaviour runner, Graphic graphic, float duration, float delay, Color color,
        Action onPlay, Action onComplete)
    {
        runner.StartCoroutine(FadeOut(graphic, duration, delay, color, onPlay, onComplete));
    }

    /// <summary>
    ///     슬라이드 애니메이션
    /// </summary>
    /// <param name="runner">MonoBehaviour를 상속한 클래스</param>
    /// <param name="image">이미지</param>
    /// <param name="duration">지속시간</param>
    /// <param name="delay">지연시간</param>
    /// <param name="fillOrigin">이동 방향</param>
    /// <param name="onPlay">시작했을 때 실행할 행동</param>
    /// <param name="onComplete">끝났을 때 실행할 행동</param>
    /// <param name="start">시작 값</param>
    /// <param name="end">끝 값</param>
    public static void SlideAnimation(MonoBehaviour runner, Image image, float duration, float delay, float start,
        float end, int fillOrigin, Action onPlay, Action onComplete)
    {
        runner.StartCoroutine(ImageSlide(image, duration, delay, start, end, fillOrigin, onPlay, onComplete));
    }

    /// <summary>
    ///     스케일X 애니메이션
    /// </summary>
    /// <param name="runner">MonoBehaviour를 상속한 클래스</param>
    /// <param name="obj">오브젝트</param>
    /// <param name="fromSize">몇에서</param>
    /// <param name="toSize">몇으로</param>
    /// <param name="duration">지속시간</param>
    /// <param name="delay">지연시간</param>
    /// <param name="onPlay">시작했을 때 실행할 행동</param>
    /// <param name="onComplete">끝났을 때 실행할 행동</param>
    public static void ScaleXAnimation(MonoBehaviour runner, GameObject obj, float fromSize, float toSize,
        float duration, float delay, Action onPlay, Action onComplete)
    {
        runner.StartCoroutine(ScaleX(obj, fromSize, toSize, duration, delay, onPlay, onComplete));
    }

    /// <summary>
    ///     스케일Y 애니메이션
    /// </summary>
    /// <param name="runner">MonoBehaviour를 상속한 클래스</param>
    /// <param name="obj">오브젝트</param>
    /// <param name="fromSize">몇에서</param>
    /// <param name="toSize">몇으로</param>
    /// <param name="duration">지속시간</param>
    /// <param name="delay">지연시간</param>
    /// <param name="onPlay">시작했을 때 실행할 행동</param>
    /// <param name="onComplete">끝났을 때 실행할 행동</param>
    public static void ScaleYAnimation(MonoBehaviour runner, GameObject obj, float fromSize, float toSize,
        float duration, float delay, Action onPlay, Action onComplete)
    {
        runner.StartCoroutine(ScaleY(obj, fromSize, toSize, duration, delay, onPlay, onComplete));
    }

    #endregion
}