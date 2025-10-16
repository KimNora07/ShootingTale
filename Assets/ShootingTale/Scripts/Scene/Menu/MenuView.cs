// System

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// UnityEngine

namespace Scene.Menu
{
    [Serializable]
    public class Point
    {
        public List<RectTransform> points;
    }


    public class MenuView : MonoBehaviour, IMenuView
    {
        [SerializeField] private RectTransform interactButton;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private List<Point> points;

        [SerializeField] private RectTransform iconButton;
        [SerializeField] private TextMeshProUGUI descriptionText;

        [SerializeField] private RectTransform title;
        [SerializeField] private RectTransform menuPanel;
        [SerializeField] private RectTransform mainSelectBar;
        [SerializeField] private RectTransform settingSelectBar;
        [SerializeField] private RectTransform otherSelectBar;
        [SerializeField] private Image fadeImage;

        private MenuPresenter _presenter;

        private void Awake()
        {
            _presenter = new MenuPresenter(this, new MenuModel());

            AnimationUtility.FadeOutAnimation(this, fadeImage, 2f, 0.5f, new Color(0, 0, 0), null, () =>
            {
                AnimationUtility.MoveAnimation(this, title, 0.5f, 0f, new Vector2(0, 0));
                AnimationUtility.MoveAnimation(this, mainSelectBar, 0.5f, 0f, new Vector2(0f, 40f));
            });
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _presenter.MoveLeft();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _presenter.MoveRight();
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                _presenter.ConfirmFromBar();
            }
        }

        public void FadeIn(float duration, float delay, Action onPlay, Action onComplete)
        {
            AnimationUtility.FadeInAnimation(this, fadeImage, duration, delay, Color.black, onPlay, onComplete);
        }

        public void FadeOut(float duration, float delay, Action onPlay, Action onComplete)
        {
            AnimationUtility.FadeOutAnimation(this, fadeImage, duration, delay, Color.black, onPlay, onComplete);
        }

        public void UpdateMenuText(string text)
        {
            buttonText.text = text;
        }

        public void UpdateDescriptionText(string text)
        {
            descriptionText.text = text;
        }

        public void RightMoveButton(Vector3 position, Action onPlay, Action[] onComplete)
        {
            AnimationUtility.MoveAnimation(this, interactButton, 0.3f, 0, position, () =>
            {
                AnimationUtility.FadeOutAnimation(this, buttonText, 0.15f, 0, new Color(1, 1, 1), onPlay, () =>
                {
                    onComplete[1]?.Invoke();
                    AnimationUtility.FadeInAnimation(this, buttonText, 0.15f, 0, new Color(1, 1, 1), null, null);
                });
            }, () => { onComplete[0]?.Invoke(); });
        }

        public void LeftMoveButton(Vector3 position, Action onPlay, Action[] onComplete)
        {
            AnimationUtility.MoveAnimation(this, interactButton, 0.3f, 0, position, () =>
            {
                AnimationUtility.FadeOutAnimation(this, buttonText, 0.15f, 0, new Color(1, 1, 1), onPlay, () =>
                {
                    onComplete[1]?.Invoke();
                    AnimationUtility.FadeInAnimation(this, buttonText, 0.15f, 0, new Color(1, 1, 1), null, null);
                });
            }, () => { onComplete[0]?.Invoke(); });
        }

        // public void PlayMoveSound()
        // {
        //     AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonMove);
        // }

        // public void PlayClickSound()
        // {
        //     AudioManager.Instance.PlaySfx(AudioManager.Instance.buttonClick);
        // }

        public void ChangeTo(RectTransform fromTarget, RectTransform toTarget, Vector2 fromTargetPosition,
            Vector2 toTargetPosition, Action onTransitionComplete, Action onComplete)
        {
            AnimationUtility.MoveAnimation(this, fromTarget, 0.5f, 0f, fromTargetPosition, null, () =>
            {
                onTransitionComplete?.Invoke();
                AnimationUtility.MoveAnimation(this, toTarget, 0.5f, 0f, toTargetPosition, null,
                    () => { onComplete?.Invoke(); });
            });
        }

        public void ChangeToBattleScene()
        {
            SceneLoader.LoadSceneAsync(this, SceneName.InGameScene);
        }

        public void TitleMoveAnimation(RectTransform target, Vector2 targetPosition)
        {
            AnimationUtility.MoveAnimation(this, target, 0.5f, 0f, targetPosition);
        }

        public void IconMoveAnimation()
        {
            AnimationUtility.ScaleYAnimation(this, iconButton.gameObject, 1.75f, 1f, 0.15f, 0,
                () =>
                {
                    AnimationUtility.ScaleXAnimation(this, iconButton.gameObject, 0.25f, 1f, 0.15f, 0, null, null);
                },
                () => { iconButton.localScale = new Vector3(1f, 1f, 1f); });
        }

        public List<Point> GetPoints() => points;
        public RectTransform GetInteractButton() => interactButton;
        public RectTransform GetIconButton() => iconButton;
        public RectTransform GetTitle() => title;
        public RectTransform GetMenuPanel() => menuPanel;
        public RectTransform GetMainSelectBar() => mainSelectBar;
        public RectTransform GetSettingSelectBar() => settingSelectBar;
        public RectTransform GetOtherSelectBar() => otherSelectBar;
    }
}