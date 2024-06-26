//System
using System.Collections;
using System.Collections.Generic;

//UnityEngine
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using KoreanTyper;
using TMPro;

public class TextTyping : MonoBehaviour
{
    public TMP_Text message;
    [TextArea]
    public List<string> originText;

    public DOTweenAnimation background;
    public DOTweenAnimation text;

    public void StartTyping()
    {
        StartCoroutine(TypingRoutine());
        Debug.Log("1");
    }

    public void EndTyping()
    {
        background.DORestartById("1");
        text.DORestartById("1");
        message.text = "";
    }

    IEnumerator TypingRoutine()
    {
        message.text = "";
        for (int t = 0; t < originText.Count; t++)
        {
            int strTypingLength = originText[t].GetTypingLength();

            for (int i = 0; i <= strTypingLength; i++)
            {
                message.text = originText[t].Typing(i);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        }
        EndTyping();
    }
}
