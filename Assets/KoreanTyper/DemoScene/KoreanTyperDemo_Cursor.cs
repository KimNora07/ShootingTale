﻿using System.Collections;
using KoreanTyper;
using UnityEngine;
using UnityEngine.UI;

// Add KoreanTyper namespace | 네임 스페이스 추가

//===================================================================================================================
//  Cursor Blink Demo
//  커서가 깜빡이는 데모
//===================================================================================================================
public class KoreanTyperDemo_Cursor : MonoBehaviour
{
    public Text TestText;
    private readonly char cursor_char = '|';

    private string typingText;

    //===============================================================================================================
    // Start infinity loop coroutine | 무한 반복 코루틴 시작
    //===============================================================================================================
    private void Start()
    {
        StartCoroutine(TypingCoroutine("한글을 타자기처럼 입력"));
    }

    //===============================================================================================================
    // Start infinity loop | 무한 반복 코루틴 
    //===============================================================================================================
    public IEnumerator TypingCoroutine(string str)
    {
        while (true)
        {
            //=======================================================================================================
            // Blink cursor | 커서 깜빡임
            //=======================================================================================================
            typingText = "";
            for (var waitCount = 0; waitCount < 6; waitCount++)
            {
                TestText.text = typingText + cursor_char;
                yield return new WaitForSeconds(0.25f);
                TestText.text = typingText;
                yield return new WaitForSeconds(0.25f);
            }

            //=======================================================================================================
            // Typing effect | 타이핑 효과
            //=======================================================================================================
            int strLength = str.GetTypingLength();
            for (var i = 0; i <= strLength; i++)
            {
                typingText = str.Typing(i);
                TestText.text = typingText + cursor_char;
                yield return new WaitForSeconds(0.05f);
            }

            //=======================================================================================================
            // Blink cursor | 커서 깜빡임
            //=======================================================================================================
            for (var waitCount = 0; waitCount < 6; waitCount++)
            {
                TestText.text = typingText + cursor_char;
                yield return new WaitForSeconds(0.25f);
                TestText.text = typingText;
                yield return new WaitForSeconds(0.25f);
            }
        }
    }
}