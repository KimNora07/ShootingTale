using System;
using System.Collections.Generic;
using UnityEngine;

public interface IMenuView
{
    // View에서 입력을 처리 후 Presenter에 위임
    event Action OnLeft;
    event Action OnRight;
    event Action OnConfirm;

    void UpdateMenuText(string text);
    void UpdateDescriptionText(string text);

    void FadeIn(float duration, float delay, Action onPlay = null, Action onComplete = null);
    void FadeOut(float duration, float delay, Action onPlay = null, Action onComplete = null);

    List<Point> GetPoints();
    RectTransform GetInteractButton();
    RectTransform GetIconButton();
    RectTransform GetTitle();
    RectTransform GetMenuPanel();
    RectTransform GetMainSelectBar();
    RectTransform GetSettingSelectBar();
    RectTransform GetOtherSelectBar();
}
