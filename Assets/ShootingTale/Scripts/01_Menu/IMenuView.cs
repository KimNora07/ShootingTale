// System
using System;
using System.Collections.Generic;

// UnityEngine
using UnityEngine;
using UnityEngine.UI;

public interface IMenuView
{
    void UpdateMenuText(string text);
    void MoveIcon(Vector3 position);
    void LeftMoveButton(Vector3 position, Action onPlay, Action[] onComplete);
    void RightMoveButton(Vector3 position, Action onPlay, Action[] onComplete);
    void PlayMoveSound();
    void PlayClickSound();

    void ChangeTo(RectTransform fromBar, RectTransform toBar, Vector2 fromTargetPosition, Vector2 toTargetPosition, Action onTransitionComplete);
    
    List<Point> GetPoints();
    Image GetSliderImage();
    RectTransform GetInteractButton();

    RectTransform GetTitle();
    RectTransform GetStartPanel();
    RectTransform GetExitPanel();
    RectTransform GetMainSelectBar();
    RectTransform GetSettingSelectBar();
    RectTransform GetOtherSelectBar();
}
