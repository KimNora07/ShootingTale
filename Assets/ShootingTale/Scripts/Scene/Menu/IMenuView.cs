// System

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// UnityEngine

namespace Scene.Menu
{
    public interface IMenuView
    {
        void UpdateMenuText(string text);
        void UpdateDescriptionText(string text);
        void LeftMoveButton(Vector3 position, Action onPlay, Action[] onComplete);
        void RightMoveButton(Vector3 position, Action onPlay, Action[] onComplete);
        void PlayMoveSound();
        void PlayClickSound();

        void ChangeTo(RectTransform fromTarget, RectTransform toTarget, Vector2 fromTargetPosition,
            Vector2 toTargetPosition, Action onTransitionComplete, Action onComplete);

        void TitleMoveAnimation(RectTransform target, Vector2 targetPosition);
        void IconMoveAnimation();

        List<Point> GetPoints();
        Image GetSliderImage();
        RectTransform GetInteractButton();
        RectTransform GetIconButton();

        RectTransform GetTitle();
        RectTransform GetMenuPanel();
        RectTransform GetMainSelectBar();
        RectTransform GetSettingSelectBar();
        RectTransform GetOtherSelectBar();
    }
}