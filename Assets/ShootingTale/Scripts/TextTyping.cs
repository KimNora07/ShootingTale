//System
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//UnityEngine
using UnityEngine;
using UnityEngine.SceneManagement;
using KoreanTyper;
using TMPro;

public class TextTyping : MonoBehaviour
{
    public TMP_Text message;
    [TextArea]
    public List<string> originText;

    private AnimationManager _animationManager;
    private MenuMain _menuMain;
    
    public Image background;
    public TMP_Text text;

    private void Awake()
    {
        _animationManager = GetComponent<AnimationManager>();
        _menuMain = GetComponent<MenuMain>();
    }
    public void StartTyping()
    {
        StartCoroutine(TypingRoutine());
    }

    private void EndTyping()
    {
        _animationManager.FadeOutAnimation(background, 1, 0, Color.white, null, () =>
        {
            _menuMain.EndHowToPlay();
        });
        _animationManager.FadeOutAnimation(text, 1, 0, new Color(0.333f, 0.333f, 0.333f), null, null);
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
