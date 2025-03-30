//System

using System;
using System.Collections;

//UnityEngine
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimationManager : MonoBehaviour
{
    #region OutQuad Type MoveAnimation
    public void MoveAnimation(RectTransform rect, float duration, float delay, Vector2 toMove, Action onPlay = null, Action onComplete = null)
    {
        StartCoroutine(Move(rect, duration, delay, toMove, onPlay, onComplete));
    }

    private static IEnumerator Move(RectTransform rect, float duration, float delay, Vector2 toMove, Action onPlay = null, Action onComplete = null)
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
    #endregion

    public void FadeInAnimation(Image image, float duration, float delay, Color color)
    {
        StartCoroutine(FadeIn(image, duration, delay, color));
    }
    public void FadeInAnimation(Image image, float duration, float delay, Color color, Action onPlay, Action onComplete)
    {
        StartCoroutine(FadeIn(image, duration, delay, color, onPlay, onComplete));
    }
    public void FadeInAnimation(TMP_Text text, float duration, float delay, Color color, Action onPlay, Action onComplete)
    {
        StartCoroutine(FadeIn(text, duration, delay, color, onPlay, onComplete));
    }

    public void FadeOutAnimation(Image image, float duration, float delay, Color color)
    {
        StartCoroutine(FadeOut(image, duration, delay, color));
    }
    public void FadeOutAnimation(Image image, float duration, float delay, Color color, Action onPlay, Action onComplete)
    {
        StartCoroutine(FadeOut(image, duration, delay, color, onPlay, onComplete));
    }
    public void FadeOutAnimation(TMP_Text text, float duration, float delay, Color color, Action onPlay, Action onComplete)
    {
        StartCoroutine(FadeOut(text, duration, delay, color, onPlay, onComplete));
    }
    
    private static IEnumerator FadeIn(Image image, float duration, float delay, Color color)
    {
        yield return new WaitForSeconds(delay);
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += 0.016f;
            yield return new WaitForSeconds(0.016f);
            
            float t = Mathf.Clamp01(elapsedTime / duration);
            float easedT = 1 - Mathf.Pow(1 - t, 2);

            image.color = new Color(color.r, color.g, color.b, easedT);
        }
    }
    
    private static IEnumerator FadeIn(TMP_Text text, float duration, float delay, Color color)
    {
        yield return new WaitForSeconds(delay);
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += 0.016f;
            yield return new WaitForSeconds(0.016f);
            
            float t = Mathf.Clamp01(elapsedTime / duration);
            float easedT = 1 - Mathf.Pow(1 - t, 2);

            text.color = new Color(color.r, color.g, color.b, easedT);
        }
    }
    
    private static IEnumerator FadeIn(Image image, float duration, float delay, Color color, Action onPlay, Action onComplete)
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

            image.color = new Color(color.r, color.g, color.b, easedT);
        }
        onComplete?.Invoke();
    }
    
    private static IEnumerator FadeIn(TMP_Text text, float duration, float delay, Color color, Action onPlay, Action onComplete)
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

            text.color = new Color(color.r, color.g, color.b, easedT);
        }
        onComplete?.Invoke();
    }

    private static IEnumerator FadeOut(Image image, float duration, float delay, Color color)
    {
        yield return new WaitForSeconds(delay);
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += 0.016f;
            yield return new WaitForSeconds(0.016f);
            
            float t = Mathf.Clamp01(elapsedTime / duration);
            float easedT = 1 - Mathf.Pow(1 - t, 2);

            image.color = new Color(color.r, color.g, color.b, 1 - easedT);
        }
    }
    
    private static IEnumerator FadeOut(Image image, float duration, float delay, Color color, Action onPlay, Action onComplete)
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

            image.color = new Color(color.r, color.g, color.b, 1 - easedT);
        }
        onComplete?.Invoke();
    }
    
    private static IEnumerator FadeOut(TMP_Text text, float duration, float delay, Color color, Action onPlay, Action onComplete)
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

            text.color = new Color(color.r, color.g, color.b, 1 - easedT);
        }
        onComplete?.Invoke();
    }
}
